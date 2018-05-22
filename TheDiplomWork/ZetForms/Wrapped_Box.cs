using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
namespace TheDiplomWork
{
    class Wrapped_Box
    {
        public enum Types { doubler, floater, integer, booler }
        

        object obj;
        public Wrapped_Box(object inp)
        {
            obj = inp;
        }
        public List<Boxed> Boxes { get; } = new List<Boxed>();

        public void Wrapped_Transfer_or_Die()
        {
            foreach (var item in Boxes) item.Transfer_Or_Error(obj);
        }

        public void Add(TextBox Box, string name, Types type)
        {
            Boxes.Add(new Boxed(obj, Box, name, type));
        }

        public void RegulatingAdvancedSectorVisilibility()
        {
            foreach (var item in Boxes)
            {
                string str = item.Get_Name();
                if (!str.Contains("Time"))
                    item.SetVisibility(Projectile.settings.AdvancedPhysics);
            }
        }

        public void Process_Text_Changed(TextBox Boxik)
        {
            try
            {
                Boxes[Boxes.FindIndex(x => x.Get_Name().Contains(Boxik.Name))].TrySetNewValue();
            }
            catch (Exception) { }
        }


        public class Boxed
        {
            
            TextBox Box;
            string name;
            Types type;
            bool InitializedWithSomethingNew = false;
            public string Get_Name() { return Box.Name; }
            public static object GetPropValue(object src, string propName)
            {
                return src.GetType().GetProperty(propName).GetValue(src, null);
            }
            public Boxed(object obj, TextBox Box, string name, Types type)
            {
                this.Box = Box;
                this.name = name;
                this.type = type;

                switch (type)
                {
                    case Types.doubler:
                        Box.Text = ((double)GetPropValue(obj, name)).ToString("G6"); break;
                    case Types.integer:
                        Box.Text = ((int)GetPropValue(obj, name)).ToString("G6"); break;
                    case Types.floater:
                        Box.Text = ((float)GetPropValue(obj, name)).ToString("G6"); break;
                    case Types.booler:
                        Box.Text = ((bool)GetPropValue(obj, name)).ToString(); break;
                }
            }
            object NewValue = 0;
            bool changed = false;
            string error_message = "";
            public static string ReplaceDecimalSeparator(string inp)
            {
                inp = inp.Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                inp = inp.Replace(".", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator);
                return inp;
            }
            public void TrySetNewValue()
            {
                if (!Form_ProjectileSettings.Initializating)
                {
                    try
                    {
                        switch (type)
                        {
                            case Types.doubler:
                                NewValue = Convert.ToDouble(ReplaceDecimalSeparator(Box.Text));
                                break;
                            case Types.integer:
                                NewValue = Convert.ToInt32(ReplaceDecimalSeparator(Box.Text)); break;
                            case Types.floater:
                                NewValue = Convert.ToSingle(ReplaceDecimalSeparator(Box.Text)); break;
                            case Types.booler:
                                NewValue = Convert.ToBoolean(ReplaceDecimalSeparator(Box.Text)); break;
                        }
                        

                        List<string>NoNegativesFor = new List<string>(new string[] { "mass", "density", "cd", "area", "timespeed" });

                        switch (type)
                        {
                            case Types.doubler: break;
                            case Types.integer:
                                if ((int)NewValue < 0) throw new Exception("Отрицательное значение"); break;
                            case Types.floater:
                                if ((float)NewValue < 0) throw new Exception("Отрицательное значение"); break;
                            case Types.booler:
                                break;
                        }


                        foreach (var item in NoNegativesFor)
                            if (name == item)
                            {
                                switch (type)
                                {
                                    case Types.doubler:
                                        if ((double)NewValue < 0) throw new Exception("Отрицательное значение"); break;
                                    case Types.integer:
                                        if ((int)NewValue < 0) throw new Exception("Отрицательное значение"); break;
                                    case Types.floater:
                                        if ((float)NewValue < 0) throw new Exception("Отрицательное значение"); break;
                                    case Types.booler:
                                        break;
                                }
                                break;
                            }
                        changed = true;
                    }
                    catch (Exception exc)
                    {
                        changed = false;
                        error_message = exc.Message;
                    }
                    InitializedWithSomethingNew = true;
                }
            }

            public static void SetPropValue(object src, string propName, object value)
            {
                try
                {
                    src.GetType().GetProperty(propName).SetValue(src, value);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Error!");
                }
            }
            public void Transfer_Or_Error(object obj)
            {
                if (InitializedWithSomethingNew)
                {
                    if (changed)
                    {
                        SetPropValue(obj, name, NewValue);
                    }
                    else MessageBox.Show(error_message, "Warning: " + Box.Name);
                }
            }
            public void SetVisibility(bool value) { Box.Visible = value; }


        }
    }
}
