using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public HolesLinkedList mMem = new HolesLinkedList();
        private ProcessLinkedList mPro = new ProcessLinkedList();
        private string mTxt1Hole = "Hole    from    ";
        private int mHoleNum;
        public string mErrorMsg="";
        private int mNumProcess;


        public Form1()
        {
            InitializeComponent();
            mNumProcess = 1;
            mHoleNum = 0;
            numericUpDown1.Minimum = 0;
            numericUpDown2.Minimum = 1;
            numericUpDown3.Minimum = 1;
            numericUpDown1.Maximum = int.MaxValue;
            numericUpDown2.Maximum = int.MaxValue;
            numericUpDown3.Maximum = int.MaxValue;
            disProcess();
        }
        private void disProcess()
        {
            textBox1.Enabled = false;
            numericUpDown3.Enabled = false;
            comboBox1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
        }
        private void enProcess()
        {
            textBox1.Enabled = true;
            textBox1.Text = "p" + mNumProcess;
            numericUpDown3.Enabled = true;
            comboBox1.Enabled = true;
            comboBox1.Text = "First Fit";
            button2.Enabled = true;
            button3.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            int start = Convert.ToInt32(numericUpDown1.Value);
            int size = Convert.ToInt32(numericUpDown2.Value);
            int end = start + size - 1;
            mMem.sort(0);
            int[] startArr = mMem.allEle(0);
            int[] endArr = mMem.allEle(1);
            int limit = mMem.size();
            bool ok = true;
            for (int i = 0; i < limit; i++)
            {
                if (end <= endArr[i] && start >= startArr[i])
                {
                    mErrorMsg = "already hole";
                    MessageBox.Show(mErrorMsg);
                    ok = false;
                    break;
                }
                else if ((i < limit - 1) &&
                            (end + 1 == startArr[i + 1] && start - 1 == endArr[i]))
                {
                    hole tmpH = mMem.findByStart(startArr[i]);
                    hole tmpH2 = mMem.findByStart(startArr[i + 1]);
                    tmpH.set_end(tmpH2.get_end());
                    tmpH.set_size(size + tmpH.get_size() + tmpH2.get_size());
                    mMem.removeByStart(startArr[i + 1]);
                    ok = false;
                    break;
                }
                else if (end + 1 == startArr[i] || (end >= startArr[i] && end <= endArr[i]))
                {
                    hole tmp = mMem.findByStart(startArr[i]);
                    tmp.set_start(start);
                    tmp.set_size(endArr[i] - start + 1);
                    ok = false;
                    break;
                }
                else if (start - 1 == endArr[i] || (start <= endArr[i] && start >= startArr[i]))
                {
                    hole tmp = mMem.findByEnd(endArr[i]);
                    for (int j = i + 1; j < limit; j++)
                    {
                        if (end >= endArr[j]) {
                            hole tmp2 = mMem.findByEnd(endArr[j]);
                            mMem.remove(tmp2);
                        }
                        else if (end >= startArr[j])
                        {
                            end = endArr[j];
                            hole tmp2 = mMem.findByStart(startArr[j]);
                            mMem.remove(tmp2);
                        }
                        else { break; }
                    }
                    tmp.set_end(end);
                    tmp.set_size(end - startArr[i] + 1);
                    ok = false;
                    break;
                }

            }
            if (ok)
            {
                mMem.Add(mHoleNum, start, size);
                enProcess();
                mHoleNum++;
            }
            printHoles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            enHole();
            listBox1.Items.Clear();
            mMem = new HolesLinkedList();
            listBox2.Items.Clear();
            mPro = new ProcessLinkedList();
            mNumProcess = 1;
            disProcess();
        }

        private void disHole()
        {
            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            button1.Enabled = false;
        }
        private void enHole()
        {
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
            button1.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool ok = true;
            disHole();
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            string proName = textBox1.Text;
            int proSize = Convert.ToInt32(numericUpDown3.Value);

            if (comboBox1.Text == "First Fit")
            {
                int[] arr = mMem.allEle(2);
                int holeNum = mMem.size();
                for (int i = 0; i < holeNum; i++)
                {
                    if (arr[i] >= proSize)
                    {
                        mNumProcess++;
                        hole tmp = mMem.findBySize(arr[i]);
                        int proStart = tmp.get_start();
                        int proEnd = proStart + proSize - 1;
                        mPro.Add(proName, proStart, proSize, tmp.get_holeNum());
                        int tmpSize = tmp.get_size();
                        int newSize = tmpSize - proSize;
                        if (newSize != 0)
                        {
                            tmp.set_start(proStart + proSize);
                            tmp.set_size(tmpSize - proSize);
                        }
                        else
                        {
                            mMem.remove(tmp);
                            mHoleNum--;
                        }
                        ok = false;
                        break;
                    }
                    else ok = true;
                }
            }
            else if (comboBox1.Text == "Best Fit")
            {
                mMem.sort(2);
                int[] arr = mMem.allEle(2);
                int holeNum = mMem.size();
                for (int i = 0; i < holeNum; i++)
                {
                    if (arr[i] >= proSize)
                    {
                        mNumProcess++;
                        hole tmp = mMem.findBySize(arr[i]);
                        int proStart = tmp.get_start();
                        int proEnd = proStart + proSize - 1;
                        mPro.Add(proName, proStart, proSize, tmp.get_holeNum());
                        int tmpSize = tmp.get_size();
                        int newSize = tmpSize - proSize;
                        if (newSize != 0)
                        {
                            tmp.set_start(proStart + proSize);
                            tmp.set_size(tmpSize - proSize);
                        }
                        else
                        {
                            mMem.remove(tmp);
                            mHoleNum--;
                        }
                        ok = false;
                        break;
                    }
                    else ok = true;
                }
            }
            else if (comboBox1.Text == "Worst Fit")
            {
                mMem.sort(2);
                int[] arr = mMem.allEle(2);
                int holeNum = mMem.size();
                if (holeNum == 0) ;
                else if (arr[holeNum - 1] >= proSize)

                {
                    mNumProcess++;
                    hole tmp = mMem.findBySize(arr[holeNum - 1]);
                    int proStart = tmp.get_start();
                    int proEnd = proStart + proSize - 1;
                    mPro.Add(proName, proStart, proSize, tmp.get_holeNum());
                    int tmpSize = tmp.get_size();
                    int newSize = tmpSize - proSize;
                    if (newSize != 0)
                    {
                        tmp.set_start(proStart + proSize);
                        tmp.set_size(tmpSize - proSize);
                    }
                    else
                    {
                        mMem.remove(tmp);
                        mHoleNum--;
                    }
                    ok = false;
                }
                else {
                    ok = true;
                }
            }
            if (ok)
            {
                mErrorMsg = "Thers is no enough memory";
                MessageBox.Show(mErrorMsg);
            }
            textBox1.Text = "p" + mNumProcess;

            printPro();
        }
        public void printPro()
        {
            mPro.sort(0);
            process pro = mPro.get_hProcess();
            int size = mPro.size();
            for(int i = 0; i < size; i++) {
                string proName = pro.get_processName();
                int start = pro.get_start();
                int end = pro.get_end();
                listBox2.Items.Add(proName + "    from    " +start+"    to    "+end);
                pro=pro.get_next();
            }
            printHoles();
        }
        public void printHoles()
        {
            mMem.sort(0);
            hole holes = mMem.get_hHole();
            int size = mMem.size();
            for (int i = 0; i < size; i++)
            {
                int start = holes.get_start();
                int end = holes.get_end();
                listBox1.Items.Add(mTxt1Hole + start + "    to    " + end);
                holes = holes.get_next();
            }

        }

        private void listBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int ind = listBox2.IndexFromPoint(e.X, e.Y);
            string tmp = listBox2.Items[ind].ToString();
            int indexOfSpace = tmp.IndexOf(" ", 0);
            string str = tmp.Substring(0, indexOfSpace);
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            process p = mPro.findByName(str);
            int holeNum = p.get_holeNum();
            hole h = mMem.findByHoleNum(holeNum);
            int proEnd = p.get_end();
            int proStart = p.get_start();
            int proSize = p.get_size();

            mMem.sort(0);
            int[] startArr = mMem.allEle(0);
            int[] endArr = mMem.allEle(1);
            int limit = mMem.size();
            bool ok = true;
            for (int i = 0; i < limit; i++)
            {
                if (proEnd <= endArr[i] && proStart >= startArr[i])
                {
                    mErrorMsg = "already hole";
                    MessageBox.Show(mErrorMsg);
                    ok = false;
                    break;
                }
                else if ((i < limit - 1) &&
                    (proEnd + 1 == startArr[i + 1] && proStart - 1 == endArr[i]))
                {
                    hole tmpH = mMem.findByStart(startArr[i]);
                    hole tmpH2 = mMem.findByStart(startArr[i + 1]);
                    tmpH.set_end(tmpH2.get_end());
                    tmpH.set_size(proSize + tmpH.get_size() + tmpH2.get_size());
                    mMem.removeByStart(startArr[i + 1]);
                    ok = false;
                    break;
                }
                else if (proEnd + 1 == startArr[i] || (proEnd >= startArr[i] && proEnd <= endArr[i]))
                {
                    hole tmpH = mMem.findByStart(startArr[i]);
                    tmpH.set_start(proStart);
                    tmpH.set_size(endArr[i] - proStart + 1);
                    ok = false;
                    break;
                }
                else if (proStart - 1 == endArr[i] || (proStart <= endArr[i] && proStart >= startArr[i]))
                {
                    hole tmpH = mMem.findByEnd(endArr[i]);
                    tmpH.set_end(proEnd);
                    tmpH.set_size(proEnd - startArr[i] + 1);
                    ok = false;
                    break;
                }

            }
            if (ok)
            {
                mMem.Add(mHoleNum, proStart, proSize);
                enProcess();
                mHoleNum++;
            }

            mPro.remove(p);
            if (mPro.size() == 0) enHole();
            printPro();
        }
    }
}
