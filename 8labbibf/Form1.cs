using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using _8labbib;
using Microsoft.VisualBasic.CompilerServices;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.Linq.Expressions;
using Accessibility;

namespace Form1
{
    public partial class Form1 : Form
    {
        Ellipse ellipse;
        bool flag = true;
        string name;
        //private Stack<Operator> Operator;
        //private Stack<Operand> Operand;
        private Stack<Operator> operators = new Stack<Operator>();
        private PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxInputString;
        private RichTextBox richTextBox1;
        private Stack<Operand> operands = new Stack<Operand>();
        //private object operands;

        public Form1()
        {
            InitializeComponent();
            Bitmap bitmap = new Bitmap(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            Pen pen = new Pen(Color.Black, 1);
            Init.bitmap = bitmap;
            Init.pen = pen;
            Init.pictureBox = pictureBox1;
        }
        private bool IsNotOperation(char item)
        {
            if (!(item == 'E' || item == 'M' || item == 'D' || item == ',' || item == '(' || item == ')'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int ConvertCharToInt(object item)
        {
            return Convert.ToInt32(Convert.ToString(item));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void textBoxInputString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                operators = new Stack<Operator>();
                operands = new Stack<Operand>();
                for (int i = 0; i < textBoxInputString.Text.Length; i++)
                {
                    if (IsNotOperation(textBoxInputString.Text[i]))
                    {
                        if (!(Char.IsDigit(textBoxInputString.Text[i])))
                        {
                            this.operands.Push(new Operand(textBoxInputString.Text[i]));
                            continue;
                        }
                        else if (Char.IsDigit(textBoxInputString.Text[i]))
                        {
                            if (flag)
                            {
                                this.operands.Push(new Operand(textBoxInputString.Text[i]));
                            }
                            else
                            {
                                if (!(Char.IsDigit(textBoxInputString.Text[i - 1])))
                                {
                                    this.operands.Push(new Operand(ConvertCharToInt(textBoxInputString.Text[i])));
                                    continue;
                                }
                                this.operands.Push(new Operand(ConvertCharToInt(this.operands.Pop().value) * 10 + ConvertCharToInt(textBoxInputString.Text[i])));
                            }
                            flag = false;
                            continue;
                        }
                    }
                    else if (textBoxInputString.Text[i] == ',')
                    {
                        flag = true;
                        continue;
                    }

                    else if (textBoxInputString.Text[i] == 'E')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBoxInputString.Text[i]));
                        continue;
                    }
                    else if (textBoxInputString.Text[i] == 'M')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBoxInputString.Text[i]));
                        continue;
                    }
                    else if (textBoxInputString.Text[i] == 'D')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBoxInputString.Text[i]));
                        continue;
                    }
                    else if (textBoxInputString.Text[i] == '(')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBoxInputString.Text[i]));
                    }
                    else if (textBoxInputString.Text[i] == ')')
                    {
                        do
                        {
                            if (operators.Peek().symbolOperator == '(')
                            {
                                operators.Pop();
                                break;
                            }
                            if (operators.Count == 0)
                            {
                                break;
                            }
                        }
                        while (operators.Peek().symbolOperator != '(');
                    }
                }
                try
                {
                    this.SelectingPerformingOperation(operators.Peek());
                }
                catch
                {
                    MessageBox.Show("Введенной операции не существует");
                    richTextBox1.Text += "Ошибка в данных\n";
                }
            }
        }


        private void SelectingPerformingOperation(Operator op)
        {
            if (op.symboloperands == 'E')
            {
                if (this.operands.Count == 4)
                {
                    int x = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int y = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int w = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int h = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    try
                    {
                        ellipse = new Ellipse(Convert.ToInt32(Convert.ToString(operands.Pop().value)), Convert.ToInt32(Convert.ToString(operands.Pop().value)), Convert.ToInt32(Convert.ToString(operands.Pop().value)), Convert.ToInt32(Convert.ToString(operands.Pop().value)));

                        op = new Operator(ellipse.Draw, 'E');
                        ShapeContainer.AddFigure(ellipse);
                        richTextBox1.Text += Ellipse.createellipse;
                        op.operatorMethod();
                        ellipse.Draw();

                    }


                    catch
                    {
                        MessageBox.Show("Неверно! Введите данные повторно");
                        richTextBox1.Text += "Ошибка в данных\n";
                    }
                }
                else
                {
                    MessageBox.Show("Количество параметров должно быть равно четырем\n");
                }
            }
            if (textBoxInputString.Text[0] == 'M')
            {
                try
                {
                    int y = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int x = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    name = Convert.ToString(operands.Pop().value);
                    string movename = "Эллипс " + name + " переместился\n";
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        MessageBox.Show("Проверьте вводимые данные\n");
                        richTextBox1.Text += "Ошибка в данных\n";
                    }
                    else
                    {
                        ShapeContainer.FindFigure(name).MoveTo(x, y);
                        richTextBox1.Text += movename;
                    }
                }
                catch
                {
                    MessageBox.Show("Неверно! Введите данные повторно");
                    richTextBox1.Text += "Ошибка в данных\n";
                }
            }
            if (textBoxInputString.Text[0] == 'D')
            {
                try
                {
                    name = Convert.ToString(operands.Pop().value);
                    string deletename = "Эллипс " + name + "удалился\n ";
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        MessageBox.Show("Проверьте вводимые данные\n");
                        richTextBox1.Text += "Ошибка в данных\n";
                    }
                    else
                    {
                        ShapeContainer.FindFigure(name).DeleteF(ShapeContainer.FindFigure(name), true);
                        richTextBox1.Text += ShapeContainer.FindFigure(name) + deletename;
                    }
                }
                catch
                {
                    MessageBox.Show("Неверно! Введите данные повторно");
                    richTextBox1.Text += "Ошибка в данных\n";
                }
            }

        }

       /* private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxInputString = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(366, 341);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // textBoxInputString
            // 
            this.textBoxInputString.Location = new System.Drawing.Point(17, 10);
            this.textBoxInputString.Name = "textBoxInputString";
            this.textBoxInputString.Size = new System.Drawing.Size(361, 23);
            this.textBoxInputString.TabIndex = 1;
            this.textBoxInputString.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(415, 10);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(199, 96);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(626, 434);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.textBoxInputString);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       */
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}