using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using _8labbib;

namespace lab9
{
    public partial class Form1 : Form
    {
        Rectagle Rect;
        bool flag = true;
        string name;
        private Stack<Operator> operators = new Stack<Operator>();
        private Stack<Operand> operands = new Stack<Operand>();

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
            if (!(item == 'S' || item == 'M' || item == 'I' || item == ',' || item == '(' || item == ')' || item=='D'))
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




        private void textBoxInputString_KeyDown_1(object sender, KeyEventArgs e)
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

                    else if (textBoxInputString.Text[i] == 'S')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBoxInputString.Text[i]));
                        continue;
                    }
                    else if (textBoxInputString.Text[i] == 'M')
                    {
                        this.operators.Push(OperatorContainer.FindOperator(textBoxInputString.Text[i]));
                        continue;
                    }
                    else if (textBoxInputString.Text[i] == 'I')
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
                    MessageBox.Show("Проверь операцию!!!");
                    richTextBox1.Text += "Ошибка!\n";
                }
            }
        }


        private void SelectingPerformingOperation(Operator op)
        {
            if (textBoxInputString.Text[0] == 'S')
            {
                if (this.operands.Count == 4)
                { 
                    int w = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    
                    int y = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int x = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    string name = Convert.ToString(operands.Pop().value);
                    try
                    {
                        
                        Rect = new Rectagle(name, x, y, w);
                        op = new Operator(Rect.Draw, 'S');
                        ShapeContainer.AddFigure(Rect);
                        richTextBox1.Text += "Квадрат " + name + " создался\n";
                        op.operatorMethod();

                    }

                    catch
                    {
                        MessageBox.Show("попробуй еще раз,по-человечески");
                        richTextBox1.Text += "Заново!!!!\n";
                    }
                }
                else
                {
                    MessageBox.Show("Проверь кол-во!Оно должно равняться 4\n");
                }
            }
            if (textBoxInputString.Text[0] == 'M')
            {
                try
                {
                    int y = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    int x = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    name = Convert.ToString(operands.Pop().value);
                    string movename = "Квадрат " + name + " переместился\n";
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        MessageBox.Show("проверь(\n");
                        richTextBox1.Text += "Ошибка!!!\n";
                    }
                    else
                    {
                        ShapeContainer.FindFigure(name).MoveTo(x, y);
                        richTextBox1.Text += movename;
                    }
                }
                catch
                {
                    MessageBox.Show("Попробуй заново");
                    richTextBox1.Text += "Ошибка в данных\n";
                }
            }
            if (textBoxInputString.Text[0] == 'I')
            {
                try
                {
                    int w = Convert.ToInt32(Convert.ToString(operands.Pop().value));
                    name = Convert.ToString(operands.Pop().value);
                    string izmname = "Квадрат " + name + " изменился\n ";
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        MessageBox.Show("Проверь(\n");
                        richTextBox1.Text += "Ошибка!!!х\n";
                    }
                    else
                    {
                        ShapeContainer.FindFigure(name).Izm(w);
                        richTextBox1.Text += izmname;
                    }
                }
                catch
                {
                    MessageBox.Show("Попробуй заново(");
                    richTextBox1.Text += "Ошибка!!!\n";
                }
            }
            if (textBoxInputString.Text[0] == 'D')
            {
                try
                {
                    name = Convert.ToString(operands.Pop().value);
                    string deletename = "Квадрат " + name + " удалился\n ";
                    if (ShapeContainer.FindFigure(name) == null)
                    {
                        MessageBox.Show("Проверь(\n");
                        richTextBox1.Text += "Ошибка!!!х\n";
                    }
                    else
                    {
                        ShapeContainer.FindFigure(name).DeleteF(ShapeContainer.FindFigure(name), true);
                        richTextBox1.Text += ShapeContainer.FindFigure(name) + deletename;
                    }
                }
                catch
                {
                    MessageBox.Show("Попробуй заново(");
                    richTextBox1.Text += "Ошибка!!!\n";
                }
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
    

