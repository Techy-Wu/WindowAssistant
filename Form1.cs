using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//for time
using System.Threading;
//for find
using System.Runtime.InteropServices;

namespace 窗体助手
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]//指定坐标处窗体句柄
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;                             //最左坐标
            public int Top;                             //最上坐标
            public int Right;                           //最右坐标
            public int Bottom;                        //最下坐标
        }

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        const int SW_MAXIMIZE = 3;
        const int SW_MINIMIZE = 6;
        const int SW_NORMAL = 1;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, uint flags); 

        decimal t = -1;
        IntPtr obj = IntPtr.Zero;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.

            button2.Hide();
            numericUpDown1.Value = 5;

            functions(false);
        }

        private void CheckBox2_Click(object sender, EventArgs e)
        {
            select(1, 1);
        }

        private void CheckBox3_Click(object sender, EventArgs e)
        {
            select(1, 2);
        }

        private void CheckBox4_Click(object sender, EventArgs e)
        {
            select(2, 1);
        }

        private void CheckBox5_Click(object sender, EventArgs e)
        {
            select(2, 2);
        }

        private void CheckBox11_Click(object sender, EventArgs e)
        {
            select(3, 1);
        }

        private void CheckBox6_Click(object sender, EventArgs e)
        {
            select(3, 2);
        }

        private void CheckBox7_Click(object sender, EventArgs e)
        {
            select(3, 3);
        }

        private void CheckBox8_Click(object sender, EventArgs e)
        {
            select(4, 1);
        }

        private void CheckBox9_Click(object sender, EventArgs e)
        {
            select(4, 2);
        }

        private void CheckBox10_Click(object sender, EventArgs e)
        {
            select(4, 3);
        }

        void select(int type, int index)
        {
            int[] chkstate = new int[4] { -1, -1, -1, -1 };
            switch (type)
            {
                case 1:
                    //<探测方式>
                    //get situation
                    if (checkBox2.Checked == true)
                        chkstate[1] = 1;
                    else
                        chkstate[1] = 0;

                    if (checkBox3.Checked == true)
                        chkstate[2] = 1;
                    else
                        chkstate[2] = 0;

                    //one choose
                    switch(index)
                    {
                        case 1:
                            //探测顶层聚焦的窗体
                            checkBox2.CheckState = CheckState.Checked;
                            checkBox3.CheckState = CheckState.Unchecked;
                            break;

                        case 2:
                            //探测指针下方的窗体
                            checkBox2.CheckState = CheckState.Unchecked;
                            checkBox3.CheckState = CheckState.Checked;
                            break;
                    }
                    break;

                case 2:
                    //<可见性>
                    //get situation
                    if (checkBox4.Checked == true)
                        chkstate[1] = 1;
                    else
                        chkstate[1] = 0;

                    if (checkBox5.Checked == true)
                        chkstate[2] = 1;
                    else
                        chkstate[2] = 0;

                    //one choose
                    switch (index)
                    {
                        case 1:
                            //显示
                            checkBox4.CheckState = CheckState.Checked;
                            checkBox5.CheckState = CheckState.Unchecked;
                            ShowWindow(obj, SW_SHOW);
                            break;

                        case 2:
                            //隐藏
                            checkBox4.CheckState = CheckState.Unchecked;
                            checkBox5.CheckState = CheckState.Checked;
                            ShowWindow(obj, SW_HIDE);
                            break;
                    }
                    break;

                case 3:
                    //<大小>
                    //get situation
                    if (checkBox11.Checked == true)
                        chkstate[1] = 1;
                    else
                        chkstate[1] = 0;

                    if (checkBox6.Checked == true)
                        chkstate[2] = 1;
                    else
                        chkstate[2] = 0;

                    if (checkBox7.Checked == true)
                        chkstate[3] = 1;
                    else
                        chkstate[3] = 0;

                    //one choose
                    switch (index)
                    {
                        case 1:
                            //最大化
                            checkBox11.CheckState = CheckState.Checked;
                            checkBox6.CheckState = CheckState.Unchecked;
                            checkBox7.CheckState = CheckState.Unchecked;
                            ShowWindow(obj, SW_MAXIMIZE);
                            break;

                        case 2:
                            //最小化
                            checkBox11.CheckState = CheckState.Unchecked;
                            checkBox6.CheckState = CheckState.Checked;
                            checkBox7.CheckState = CheckState.Unchecked;
                            ShowWindow(obj, SW_MINIMIZE);
                            break;

                        case 3:
                            //正常大小
                            checkBox11.CheckState = CheckState.Unchecked;
                            checkBox6.CheckState = CheckState.Unchecked;
                            checkBox7.CheckState = CheckState.Checked;
                            ShowWindow(obj, SW_NORMAL);
                            break;
                    }
                    break;

                case 4:
                    //<层级顺序>
                    //get situation
                    if (checkBox8.Checked == true)
                        chkstate[1] = 1;
                    else
                        chkstate[1] = 0;

                    if (checkBox9.Checked == true)
                        chkstate[2] = 1;
                    else
                        chkstate[2] = 0;

                    if (checkBox10.Checked == true)
                        chkstate[3] = 1;
                    else
                        chkstate[3] = 0;

                    //one choose
                    switch (index)
                    {
                        case 1:
                            //移到顶端并置顶
                            checkBox8.CheckState = CheckState.Checked;
                            checkBox9.CheckState = CheckState.Unchecked;
                            checkBox10.CheckState = CheckState.Unchecked;
                            SetWindowPos(obj, -1, 0, 0, 0, 0, 1 | 2);
                            break;

                        case 2:
                            //移到顶端
                            checkBox8.CheckState = CheckState.Unchecked;
                            checkBox9.CheckState = CheckState.Checked;
                            checkBox10.CheckState = CheckState.Unchecked;
                            SetWindowPos(obj, -2, 0, 0, 0, 0, 1 | 2);
                            break;

                        case 3:
                            //移到底端
                            checkBox8.CheckState = CheckState.Unchecked;
                            checkBox9.CheckState = CheckState.Unchecked;
                            checkBox10.CheckState = CheckState.Checked;
                            SetWindowPos(obj, 1, 0, 0, 0, 0, 1 | 2 | 0x0010);
                            break;
                    }
                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //check method
            if(checkBox2.CheckState == CheckState.Unchecked && checkBox3.CheckState == CheckState.Unchecked)
            {
                MessageBox.Show("请选择探测方法", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //check state
            if(obj != IntPtr.Zero && obj != this.Handle && checkBox5.CheckState == CheckState.Checked)
            {

                DialogResult result = MessageBox.Show(string.Concat("现在操作的窗体 [", label14.Text == string.Empty ? "(无标题)" : label14.Text, "] 处于隐藏状态，重新探测后将覆盖句柄记忆，您将无法恢复 [", label14.Text == string.Empty ? "(无标题)" : label14.Text, "] 的显示，是否继续？"), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                    return;
            }

            //lock
            numericUpDown1.Enabled = false;
            button2.Show();

            t = numericUpDown1.Value;
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if(numericUpDown1.Value == 0)
            {
                timer1.Stop();

                //search
                switch(checkBox1.CheckState)
                {
                    case CheckState.Checked:
                        //探测顶层聚焦的窗体
                        obj = GetForegroundWindow();
                        break;

                    case CheckState.Unchecked:
                        //探测指针下方的窗体
                        obj = WindowFromPoint(Control.MousePosition.X, Control.MousePosition.Y);
                        break;
                }
                label2.Text = obj.ToString();
                int length = GetWindowTextLength(obj);
                StringBuilder windowName = new StringBuilder(length + 1);
                GetWindowText(obj, windowName, windowName.Capacity);
                label14.Text = windowName.ToString();

                if(obj != this.Handle)
                {
                    functions(true);
                    if (label14.Text == string.Empty)
                        MessageBox.Show("该对象没有描述标题，请谨慎操作！\n", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("探测到自身", "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    functions(false);
                }

                return;
            }

            numericUpDown1.Value--;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //stop & unlock & reset
            timer1.Stop();
            numericUpDown1.Enabled = true;
            numericUpDown1.Value = t;
            button2.Hide();
        }

        private void CheckBox1_Click(object sender, EventArgs e)
        {
            switch(checkBox1.CheckState)
            {
                case CheckState.Checked:
                    this.TopMost = true;
                    break;

                case CheckState.Unchecked:
                    this.TopMost = false;
                    break;
            }
        }

        void functions(bool s)
        {
            switch(s)
            {
                case true:
                    checkBox4.Enabled = true;
                    checkBox5.Enabled = true;
                    checkBox6.Enabled = true;
                    checkBox7.Enabled = true;
                    checkBox8.Enabled = true;
                    checkBox9.Enabled = true;
                    checkBox10.Enabled = true;
                    checkBox11.Enabled = true;
                    break;

                case false:
                    checkBox4.Enabled = false;
                    checkBox5.Enabled = false;
                    checkBox6.Enabled = false;
                    checkBox7.Enabled = false;
                    checkBox8.Enabled = false;
                    checkBox9.Enabled = false;
                    checkBox10.Enabled = false;
                    checkBox11.Enabled = false;
                    break;
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (checkBox5.CheckState == CheckState.Checked)
            {
                DialogResult result = MessageBox.Show("当前目标窗体处于隐藏状态中，退出后将无法对其进行图形交互操作，\n是否确定退出？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    e.Cancel = true;
                else
                    Application.Exit();
            }
            else
                Application.Exit();
        }
    }
}