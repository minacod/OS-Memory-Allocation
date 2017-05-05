using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{

    public class process
    {
        private process mNext;
        private string mProcessName;
        private int mStart;
        private int mSize;
        private int mEnd;
        private int mHoleNum;
        public process(string str, int startAt, int size,int hole)
        {
            mNext = null;
            mProcessName = str;
            mStart = startAt;
            mSize = size;
            mEnd = startAt + size - 1;
            mHoleNum = hole;
        }
        public void set_next(process p) { mNext = p; }
        public process get_next() { return mNext; }
        public void set_processName(string s) { mProcessName = s; }
        public string get_processName() { return mProcessName; }
        public void set_start(int i) { mStart = i; }
        public int get_start() { return mStart; }
        public void set_size(int i) { mSize = i; }
        public int get_size() { return mSize; }
        public void set_end(int i) { mEnd = i; }
        public int get_end() { return mEnd; }
        public void set_holeNum(int i) { mHoleNum = i; }
        public int get_holeNum() { return mHoleNum; }
    }
    public class hole
    {
        private hole mNext;
        private int mHoleNum;
        private int mStart;
        private int mSize;
        private int mEnd;
        public hole(int num, int startAt, int size)
        {
            mNext = null;
            mHoleNum = num;
            mStart = startAt;
            mSize = size;
            mEnd = startAt + size - 1;
        }
        public void set_next(hole p) { mNext = p; }
        public hole get_next() { return mNext; }
        public void set_holeNum(int n) { mHoleNum = n; }
        public int get_holeNum() { return mHoleNum; }
        public void set_start(int i) { mStart = i; }
        public int get_start() { return mStart; }
        public void set_size(int i) { mSize = i; }
        public int get_size() { return mSize; }
        public void set_end(int i) { mEnd = i; }
        public int get_end() { return mEnd; }
    }
    public class HolesLinkedList
    {
        private hole head;

        public HolesLinkedList()
        {
            head = null;
        }
        public bool isEmpty()
        {
            if (head == null) return true;
            else return false;
        }
        public void Add(int num, int start, int size)
        {
            hole nw = new hole(num, start, size);
            if (isEmpty()) { head = nw; }
            else
            {
                hole tmp = last();
                tmp.set_next(nw);
            }
        }
        public void sort(int sel)
        {
            hole tmp = head;
            int n = size();
            int x, y;
            if (sel == 0)
            {
                for (x = 0; x < n; x++)
                {
                    hole tmp2 = tmp.get_next();
                    for (y = 0; y < n - x - 1; y++)
                    {
                        int c = tmp.get_start();
                        int nn = tmp2.get_start();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }

                        tmp2 = tmp2.get_next();
                    }
                    tmp = tmp.get_next();
                }
            }
            else if (sel == 1)
            {
                for (x = 0; x < n; x++)
                {
                    hole tmp2 = tmp.get_next();
                    for (y = 0; y < n - x - 1; y++)
                    {
                        int c = tmp.get_end();
                        int nn = tmp2.get_end();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }

                        tmp2 = tmp2.get_next();
                    }
                    tmp = tmp.get_next();
                }
            }
            else if (sel == 2)
            {
                for (x = 0; x < n; x++)
                {
                    hole tmp2 = tmp.get_next();
                    for (y = 0; y < n - x - 1; y++)
                    {
                        int c = tmp.get_size();
                        int nn = tmp2.get_size();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }

                        tmp2 = tmp2.get_next();
                    }
                    tmp = tmp.get_next();
                }
            }
        }
        public void swap(hole tmp, hole tmp2)
        {
            int tmpHN = tmp2.get_holeNum();
            int tmpStart = tmp2.get_start();
            int tmpSize = tmp2.get_size();
            int tmpEnd = tmp2.get_end();
            tmp2.set_start(tmp.get_start());
            tmp2.set_size(tmp.get_size());
            tmp2.set_holeNum(tmp.get_holeNum());
            tmp2.set_end(tmp.get_end());
            tmp.set_start(tmpStart);
            tmp.set_size(tmpSize);
            tmp.set_holeNum(tmpHN);
            tmp.set_end(tmpEnd);

        }
        public int size()
        {
            if (isEmpty()) return 0;
            int i = 1;
            hole tmp = head.get_next();
            while (tmp != null)
            {
                i++;
                tmp = tmp.get_next();
            }
            return i;
        }
        public hole last() {
            if (isEmpty()) return null;
            hole tmp = head;
            while (tmp.get_next() != null)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public int holeSize()
        {
            if (isEmpty()) return 0;
            hole tmp = head;
            int i = 0;
            while (tmp != null)
            {
                i = i + tmp.get_size();
                tmp = tmp.get_next();
            }
            return i;
        }
        public int[] allEle(int j)
        {
            hole tmp = head;
            int s = size();
            int[] arr = new int[s];
            if (j == 0)
            {
                for (int i = 0; i < s; i++)
                {
                    arr[i] = tmp.get_start();
                    tmp = tmp.get_next();
                }
            }
            else if (j == 1)
            {
                for (int i = 0; i < s; i++)
                {
                    arr[i] = tmp.get_end();
                    tmp = tmp.get_next();
                }
            }
            else if (j == 2)
            {
                for (int i = 0; i < s; i++)
                {
                    arr[i] = tmp.get_size();
                    tmp = tmp.get_next();
                }
            }


            return arr;
        }
        public hole findByStart(int i) {
            hole tmp = head;
            while (tmp.get_start() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public hole findByEnd(int i)
        {
            hole tmp = head;
            while (tmp.get_end() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public hole findBySize(int i)
        {
            hole tmp = head;
            while (tmp.get_size() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public hole findByHoleNum(int i)
        {
            if(isEmpty()) return null;
            hole tmp = head;
            while (tmp.get_holeNum() != i&& tmp.get_next()!= null)
            {
                tmp = tmp.get_next();
            }
            if (tmp.get_holeNum() == i)
                return tmp;
            else
                return null;
        }
        public void removeByStart(int i) {
            hole tmp = findByStart(i);
            remove(tmp);
        }
            public void remove(hole h)
        {
            hole tmp = head;
            if (size() == 1)
            {
                head = null;
                return;
            }
            else if (tmp==h)
            {
                head = tmp.get_next();
                return;
            }
            else
            {

                while (tmp.get_next() != h && tmp.get_next() != null)
                {
                    tmp = tmp.get_next();
                }
                hole tmp2 = tmp.get_next();
                tmp.set_next(tmp2.get_next());
            }
        }
        public hole get_hHole()
        {
            return head;
        }
    }
    public class ProcessLinkedList
    {
        private process head;
        public ProcessLinkedList()
        {
            head = null;
        }
        public bool isEmpty()
        {
            if (head == null) return true;
            else return false;
        }
        public void Add(string str, int start, int size,int holeNum)
        {
            process nw = new process(str, start, size, holeNum);
            if (isEmpty()) { head = nw; }
            else
            {
                process tmp = last();
                tmp.set_next(nw);
            }
        }
        public void sort(int sel)
        {
            process tmp = head;
            int n = size();
            int x, y;
            if (sel == 0)
            {
                for (x = 0; x < n; x++)
                {
                    process tmp2 = tmp.get_next();
                    for (y = 0; y < n - x - 1; y++)
                    {
                        int c = tmp.get_start();
                        int nn = tmp2.get_start();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }

                        tmp2 = tmp2.get_next();
                    }
                    tmp = tmp.get_next();
                }
            }
            else if (sel == 1)
            {
                for (x = 0; x < n; x++)
                {
                    process tmp2 = tmp.get_next();
                    for (y = 0; y < n - x - 1; y++)
                    {
                        int c = tmp.get_end();
                        int nn = tmp2.get_end();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }

                        tmp2 = tmp2.get_next();
                    }
                    tmp = tmp.get_next();
                }
            }
            else if (sel == 2)
            {
                for (x = 0; x < n; x++)
                {
                    process tmp2 = tmp.get_next();
                    for (y = 0; y < n - x - 1; y++)
                    {
                        int c = tmp.get_size();
                        int nn = tmp2.get_size();
                        if (c > nn)
                        {
                            swap(tmp, tmp2);
                        }

                        tmp2 = tmp2.get_next();
                    }
                    tmp = tmp.get_next();
                }
            }
        }
        public void swap(process tmp, process tmp2)
        {
            int tmpHN = tmp2.get_holeNum();
            int tmpStart = tmp2.get_start();
            int tmpSize = tmp2.get_size();
            int tmpEnd = tmp2.get_end();
            tmp2.set_start(tmp.get_start());
            tmp2.set_size(tmp.get_size());
            tmp2.set_holeNum(tmp.get_holeNum());
            tmp2.set_end(tmp.get_end());
            tmp.set_start(tmpStart);
            tmp.set_size(tmpSize);
            tmp.set_holeNum(tmpHN);
            tmp.set_end(tmpEnd);

        }
        public int size()
        {
            if (isEmpty()) return 0;
            int i = 1;
            process tmp = head.get_next();
            while (tmp != null)
            {
                i++;
                tmp = tmp.get_next();
            }
            return i;
        }
        public process last()
        {
            if (isEmpty()) return null;
            process tmp = head;
            while (tmp.get_next() != null)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public int processlistSize()
        {
            if (isEmpty()) return 0;
            process tmp = head;
            int i = 0;
            while (tmp != null)
            {
                i = i + tmp.get_size();
                tmp = tmp.get_next();
            }
            return i;
        }
        public int[] allEle(int j)
        {
            process tmp = head;
            int s = size();
            int[] arr = new int[s];
            if (j == 0)
            {
                for (int i = 0; i < s; i++)
                {
                    arr[i] = tmp.get_start();
                    tmp = tmp.get_next();
                }
            }
            else if (j == 1)
            {
                for (int i = 0; i < s; i++)
                {
                    arr[i] = tmp.get_end();
                    tmp = tmp.get_next();
                }
            }
            else if (j == 2)
            {
                for (int i = 0; i < s; i++)
                {
                    arr[i] = tmp.get_size();
                    tmp = tmp.get_next();
                }
            }


            return arr;
        }
        public process findByStart(int i)
        {
            process tmp = head;
            while (tmp.get_start() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public process findByEnd(int i)
        {
            process tmp = head;
            while (tmp.get_end() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public process findBySize(int i)
        {
            process tmp = head;
            while (tmp.get_size() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public process findByName(string i)
        {
            process tmp = head;
            while (tmp.get_processName() != i)
            {
                tmp = tmp.get_next();
            }
            return tmp;
        }
        public void removeByStart(int i)
        {
            process tmp = findByStart(i);
            process tmp2 = tmp.get_next().get_next();
            tmp.set_next(tmp2);
        }
        public void removeByName(string name)
        {
            process tmp = head;
            if (size() == 1)
            {
                head = null;
                return;
            }
            else if (tmp.get_processName() == name)
            {
                head = tmp.get_next();
                return;
            }
            else
            {
                tmp = tmp.get_next();
                while (tmp.get_processName() != name && tmp.get_next() != null)
                {
                    tmp = tmp.get_next();

                }
                process tmp2 = tmp.get_next();
                tmp.set_next(tmp2.get_next());
            }
        }
        public void remove(process p)
        {
            process tmp = head;
            if (size() == 1)
            {
                head = null;
                return;
            }
            else if (tmp == p)
            {
                head = tmp.get_next();
                return;
            }
            else
            {

                while (tmp.get_next() != p && tmp.get_next() != null)
                {
                    tmp = tmp.get_next();
                }
                process tmp2 = tmp.get_next();
                tmp.set_next(tmp2.get_next());
            }
        }
        public process get_hProcess()
        {
            return head;
        }

    }

    static class Program
    {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
