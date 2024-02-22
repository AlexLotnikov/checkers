using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace checkers
{
    public class cell
    {
        public int x, y;
        public override bool Equals(object obj)
        {
            if(obj is cell c)
            {
                return (c.x==x &&c.y==y);
            }
           return false;

        }
    }

    public class move
    {
        public cell start, end;
        public string type;
    }
    public class game
    {
        
        public int[,] position = new int[8, 8];
        public string status = "";
        public move move = new move();
        List< move > moves = new List< move >();
        public List<cell> pm = new List<cell>();
        public List<cell> find_beat_moves(cell c)
        {
            List<cell> ans = new List<cell>();
            
            if (position[c.x, c.y] == 1)
            {
                for(int i=-1; i<2; i=i+2)
                {
                    for(int j=-1; j<2; j=j+2)
                    {
                        try
                        {
                            if ( (position[c.x + i, c.y + j] == 2 || position[c.x + i, c.y + j] == 4) && position[c.x + 2 * i, c.y + 2 * j] == 0)
                            {
                                cell go = new cell();
                                go.x = c.x + 2 * i;
                                go.y = c.y + 2 * j;
                                ans.Add(go);
                            }
                        }
                        catch( System.IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
            else if (position[c.x, c.y] == 2)
            {
                for (int i = -1; i < 2; i = i + 2)
                {
                    for (int j = -1; j < 2; j = j + 2)
                    {
                        try
                        {
                            if ( (position[c.x + i, c.y + j] == 1 || position[c.x + i, c.y + j] == 3) && position[c.x + 2 * i, c.y + 2 * j] == 0)
                            {
                                cell go = new cell();
                                go.x = c.x + 2 * i;
                                go.y = c.y + 2 * j;
                                ans.Add(go);
                            }
                        }
                        catch (System.IndexOutOfRangeException)
                        {

                        }
                    }
                }
            }
            else if (position[c.x, c.y] == 3)
            {
                for (int i = -1; i < 2; i = i + 2)
                {
                    for (int j = -1; j < 2; j = j + 2)
                    {
                        cell go  = new cell();
                        go.x = c.x + i;
                        go.y = c.y + j;
                        try
                        {
                            while (position[go.x, go.y] == 0)
                            {
                                go.x = go.x + i;
                                go.y = go.y + j;
                            }
                        }
                        catch(IndexOutOfRangeException)
                        {
                            go.x = go.x - i;
                            go.y = go.y - j;
                        }

                        if (position[go.x, go.y] == 2 || position[go.x, go.y] == 4)
                        {
                            go.x = go.x + i;
                            go.y = go.y + j;
                            try
                            {
                                while (position[go.x, go.y] == 0)
                                {
                                    cell nm = new cell();
                                    nm.x = go.x;
                                    nm.y = go.y;
                                    ans.Add(nm);
                                    go.x = go.x + i;
                                    go.y = go.y + j;
                                }
                            }
                            catch(IndexOutOfRangeException)
                            {
                                
                            }
                        }
                    }
                }
            }
            else if (position[c.x, c.y] == 4)
            {
                for (int i = -1; i < 2; i = i + 2)
                {
                    for (int j = -1; j < 2; j = j + 2)
                    {
                        cell go = new cell();
                        go.x = c.x + i;
                        go.y = c.y + j;
                        try
                        {
                            while (position[go.x, go.y] == 0)
                            {
                                go.x = go.x + i;
                                go.y = go.y + j;
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            go.x = go.x - i;
                            go.y = go.y - j;
                        }

                        if (position[go.x, go.y] == 1 || position[go.x, go.y] == 3)
                        {
                            go.x = go.x + i;
                            go.y = go.y + j;
                            try
                            {
                                while (position[go.x, go.y] == 0)
                                {
                                    cell nm = new cell();
                                    nm.x = go.x;
                                    nm.y = go.y;
                                    ans.Add(nm);
                                    go.x = go.x + i;
                                    go.y = go.y + j;
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {

                            }
                        }
                    }
                }
            }
            return ans;
        }
        public void damka_check(cell pos)
        {
            if (position[pos.x, pos.y] == 1 && pos.x==7)
            {
                position[pos.x, pos.y] = 3;
            }
            if (position[pos.x, pos.y] == 2 && pos.x == 0)
            {
                position[pos.x, pos.y] = 4;
            }
        }
        public void queens_move_check( int n)
        {

            List<cell> beat = new List<cell> ();
            List<cell> piace = new List<cell>();
            foreach(cell c in pm)
            {
                int remember = position[c.x, c.y];
                position[c.x, c.y] = n;
                if(find_beat_moves(c).Count>0)
                {
                    beat.Add(c);
                }
                else
                {
                    piace.Add(c);
                }
                position[c.x, c.y] = remember;
            }
            if(beat.Count>0)
            {
                pm.Clear();
                foreach(cell c in beat)
                {
                    pm.Add(c);
                }
            }
        }
        public List<cell> find_proper_moves(cell c)
        {
            List<cell> ans = new List<cell>();
            cell go1 = new cell();
            cell go2 = new cell();
            if (position[c.x, c.y]==1)
            {
                if(c.x==7)
                {
                    return ans;
                }
                if(c.y!=0)
                {
                    if(position[c.x+1, c.y-1] == 0)
                    {
                        go1.x = c.x + 1;
                        go1.y = c.y - 1;
                        ans.Add(go1);
                    }
                }
                if (c.y != 7)
                {
                    if (position[c.x + 1, c.y + 1] == 0)
                    {
                        go2.x = c.x + 1;
                        go2.y = c.y + 1;
                        ans.Add(go2);
                    }
                }
            }
            else if (position[c.x, c.y] == 2)
            {
                if (c.x == 0)
                {
                    return ans;
                }
                if (c.y != 0)
                {
                    if (position[c.x - 1, c.y - 1] == 0)
                    {
                        go1.x = c.x - 1;
                        go1.y = c.y - 1;
                        ans.Add(go1);
                    }
                }
                if (c.y != 7)
                {
                    if (position[c.x - 1, c.y + 1] == 0)
                    {
                        go2.x = c.x - 1;
                        go2.y = c.y + 1;
                        ans.Add(go2);
                    }
                }
            }
            else if(position[c.x, c.y] == 3 || position[c.x, c.y] == 4)
            {
                for(int i=-1; i<2; i=i+2)
                {
                    for(int j=-1; j<2; j=j+2)
                    {
                        int go = 1;
                        bool cyc = true;
                        while(cyc)
                        {
                            try
                            {
                                if (position[c.x + go * i, c.y + go * j] == 0)
                                {
                                    cell g = new cell();
                                    g.x = c.x + go * i;
                                    g.y = c.y + go * j;
                                    ans.Add(g);
                                }
                                else
                                {
                                    cyc = false;
                                }
                                go++;
                            }
                            catch(IndexOutOfRangeException)
                            {
                                cyc=false;
                            }
                        }
                    }
                }
            }

            return ans;
        }
        public void update_pm(cell c)
        {
            List<cell> pm1 = find_proper_moves(c);
            List<cell> pm2 = find_beat_moves(c);
            pm.Clear();
            string str = $"from ({c.x+1}; {c.y+1}) ";
            if (pm2.Count == 0)
            {
                foreach (cell cell in pm1)
                {
                    str = str + $" ({cell.x + 1}; {cell.y + 1}) ";
                    pm.Add(cell);
                    move.type = "peace";
                }
            }
            else
            {
                foreach (cell cell in pm2)
                {
                    str = str + $" ({cell.x + 1}; {cell.y + 1}) ";
                    pm.Add(cell);
                    move.type = "beat";
                }
            }

            if (position[c.x, c.y]==3 || position[c.x, c.y] == 4)
            {
                queens_move_check(position[c.x, c.y]);
            }
            //Form1.t.Text = str;
        }
        public bool pm_conrains(cell pos)
        {
            foreach(cell cell in pm)
            {
                if(cell.x==pos.x && cell.y==pos.y)
                {
                    return true;
                }
            }
            return false;
        }
        public void set_start_positions(int p)
        {
            List<cell> peace = new List<cell>();
            List<cell> beat = new List<cell>();
            for (int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if (position[i, j]==p || position[i, j] == p+2)
                    {
                        cell c = new cell();
                        c.x = i;
                        c.y = j;
                        if(find_beat_moves(c).Count>0)
                        {
                            beat.Add(c);
                        }
                        else if(find_proper_moves(c).Count>0)
                        {
                            peace.Add(c);
                        }
                    }
                }
            }

            string str;
            if (p == 1)
            {
                str = "white ";
            }
            else
            {
                str = "black ";
            }
            pm.Clear();
            if (beat.Count>0)
            {
                foreach(cell c in beat)
                {
                    str = str + $"({c.x}; {c.y})  ";
                    pm.Add(c);
                }
               // Form1.t.Text = str;
            }
            else if (peace.Count>0)
            {
                foreach (cell c in peace)
                {
                    str = str + $"({c.x}; {c.y})  ";
                    pm.Add(c);
                }
                try
                {
                   // Form1.t.Text = str;
                }
                catch(NullReferenceException)
                {

                }
            }
            else
            {
                Form1.t.Text = "end";
                status = "end";
            }
        }
        
        public void setStart()
        {
            for(int i=0; i<8; i++)
            {
                for(int j=0; j<8; j++)
                {
                    if( (i+j)%2==0 )
                    {
                        if(i<=2)
                        {
                            position[i, j] = 1;
                        }
                        else if (i>=5)
                        {
                            position[i, j] = 2;
                        }
                        else
                        {
                            position[i, j] = 0;
                        }
                    }
                    else
                    {
                        position[i, j] = 0;
                    }
                    
                }
            }
            set_start_positions(1);
            status = "first_pre";
        }
        public void move_ch()
        {
            position[move.end.x, move.end.y] = position[move.start.x, move.start.y];
            position[move.start.x, move.start.y] = 0;
            int i = Math.Sign(move.end.x - move.start.x);
            int j = Math.Sign(move.end.y - move.start.y);
            cell go = new cell();
            go.x = move.start.x+i;
            go.y = move.start.y+j;
            while(!(go.x==move.end.x) )
            {
                position[go.x, go.y] = 0;
                go.x = go.x + i;
                go.y = go.y + j;
            }
            damka_check(move.end);
        }
        

    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static game game = new game();
        [STAThread]
        static void Main()
        {
            game.setStart();
            game.set_start_positions(1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           
        }
    }
}
