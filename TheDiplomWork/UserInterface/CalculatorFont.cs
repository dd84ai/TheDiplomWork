using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    class CalculatorFont
    {
        Single Line_Height = 0.5f;
        float fontsize;
        OpenGL gl;
        int x, y;
        float static_step;

        void X_draw_move()
        {
            x += (int)static_step;
        }
        enum Actions
        {
            Верхняя, Нижняя, Средняя, Левая_верт_полная, Левая_верт_нижняя, Левая_верт_верхняя,
            Правая_верт_полная, Правая_верт_нижняя, Правая_верт_верхняя, Запятая, Наискосок_семерки, Х_верхне_лев, Х_верхне_прав, Х_нижн_лев, Х_нижн_прав, Б_верхняя, Б_нижняя
        }
        void Enum_act(Actions action)
        {
            switch (action)
            {
                case Actions.Левая_верт_полная:
                    Левая_верт_полная();
                    break;
                case Actions.Левая_верт_нижняя:
                    Левая_верт_нижняя();
                    break;
                case Actions.Левая_верт_верхняя:
                    Левая_верт_верхняя();
                    break;
                case Actions.Правая_верт_верхняя:
                    Правая_верт_верхняя();
                    break;
                case Actions.Правая_верт_нижняя:
                    Правая_верт_нижняя();
                    break;
                case Actions.Правая_верт_полная:
                    Правая_верт_полная();
                    break;
                case Actions.Средняя:
                    Средняя();
                    break;
                case Actions.Нижняя:
                    Нижняя();
                    break;
                case Actions.Верхняя:
                    Верхняя();
                    break;
                case Actions.Запятая:
                    Запятая();
                    break;
                case Actions.Наискосок_семерки:
                    Наискосок_семерки();
                    break;
                case Actions.Х_верхне_лев:
                    Х_верхне_лев();
                    break;
                case Actions.Х_верхне_прав:
                    Х_верхне_прав();
                    break;
                case Actions.Х_нижн_лев:
                    Х_нижн_лев();
                    break;
                case Actions.Х_нижн_прав:
                    Х_нижн_прав();
                    break;
                case Actions.Б_верхняя:
                    Б_верхняя();
                    break;
                case Actions.Б_нижняя:
                    Б_нижняя();
                    break;

            }
        }
        //Мысленно здесь класс функций калькуляторного шрифта начинается
        
            public void Левая_верт_полная()
            {
                //Левая полная черта
                gl.Vertex(x, y, Line_Height);
                gl.Vertex(x, y + fontsize, Line_Height);
            }
            public void Верхняя()
            {
                //Верхняя черта
                gl.Vertex(x, y + fontsize, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
            }
            public void Нижняя()
            {
                //Нижняя линия
                gl.Vertex(x, y, Line_Height);
                gl.Vertex(x + fontsize / 2, y, Line_Height);
            }
            public void Средняя()
            {
                //Средняя черта
                gl.Vertex(x, y + fontsize / 2, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
            }
            public void Левая_верт_нижняя()
            {
                //Левая нижняя черта
                gl.Vertex(x, y, Line_Height);
                gl.Vertex(x, y + fontsize / 2, Line_Height);
            }
            public void Левая_верт_верхняя()
            {
                //Левая верхняя черта
                gl.Vertex(x, y + fontsize, Line_Height);
                gl.Vertex(x, y + fontsize / 2, Line_Height);
            }
            public void Правая_верт_полная()
            {
                //Правая полная черта
                gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                gl.Vertex(x + fontsize / 2, y, Line_Height);
            }
            public void Правая_верт_нижняя()
            {
                //Правая нижняя черта
                gl.Vertex(x + fontsize / 2, y, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
            }
            public void Правая_верт_верхняя()
            {
                //Правая верхняя черта
                gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
            }
            public void Запятая()
            {
                gl.Vertex(x + fontsize / 4, y, Line_Height);
                gl.Vertex(x, y - fontsize / 4, Line_Height);
            }
            public void Наискосок_семерки()
            {
                //Наискосок семерки
                gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                gl.Vertex(x, y, Line_Height);
            }
            public void Х_верхне_лев()
            {
                gl.Vertex(x, y + fontsize, Line_Height);
                gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
            }
            public void Х_верхне_прав()
            {
                gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
            }
            public void Х_нижн_лев()
            {
                gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
                gl.Vertex(x, y, Line_Height);
            }
            public void Х_нижн_прав()
            {
                gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
                gl.Vertex(x + fontsize / 2, y, Line_Height);
            }
            public void Б_верхняя()
            {
                gl.Vertex(x, y + fontsize, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize * 3 / 4, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize * 3 / 4, Line_Height);
                gl.Vertex(x, y + fontsize / 2, Line_Height);
            }
            public void Б_нижняя()
            {
                gl.Vertex(x, y + fontsize / 2, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize / 4, Line_Height);
                gl.Vertex(x + fontsize / 2, y + fontsize / 4, Line_Height);
                gl.Vertex(x, y, Line_Height);
            }

        bool Its_number(char symbol)
        {
            switch (symbol)
            {
                case '0': return true;
                case '1': return true;
                case '2': return true;
                case '3': return true;
                case '4': return true;
                case '5': return true;
                case '6': return true;
                case '7': return true;
                case '8': return true;
                case '9': return true;
                case '-': return true;
                case '+': return true;
                default: return false;
            }
        }
        void Translating_Swither(char symbol)
        {
            switch (symbol)
            {
                case ',':
                    Enum_act(Actions.Запятая);
                    X_draw_move();
                    break;
                case '.':
                    Enum_act(Actions.Запятая);
                    X_draw_move();
                    break;
                case '0':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case '1':
                    Enum_act(Actions.Правая_верт_полная);
                    //Палочка однерки
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
                    X_draw_move();
                    break;
                case '2':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Левая_верт_нижняя);
                    Enum_act(Actions.Правая_верт_верхняя);

                    X_draw_move();
                    break;
                case '3':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case '4':
                    Enum_act(Actions.Правая_верт_полная);
                    Enum_act(Actions.Средняя);
                    //Наискосок чертверки
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    X_draw_move();
                    break;
                case '5':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_верхняя);
                    Enum_act(Actions.Правая_верт_нижняя);
                    X_draw_move();
                    break;
                case '6':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Правая_верт_нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    X_draw_move();
                    break;
                case '7':
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Наискосок_семерки);
                    X_draw_move();
                    break;
                case '8':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);

                    X_draw_move();
                    break;
                case '9':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Правая_верт_полная);
                    Enum_act(Actions.Левая_верт_верхняя);
                    X_draw_move();
                    break;
                case '-':
                    Enum_act(Actions.Средняя);
                    X_draw_move();
                    break;
                case '|':
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    X_draw_move();
                    break;
                case '+':
                    Enum_act(Actions.Средняя);
                    //Средняя вертикальная черта
                    gl.Vertex(x + fontsize / 4, y + fontsize / 4, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize * 3 / 4, Line_Height);
                    X_draw_move();
                    break;
                case 'e':
                    if (Bool_its_number)
                    {
                        //Левая полная черта
                        gl.Vertex(x, y, Line_Height);
                        gl.Vertex(x, y + fontsize / 2, Line_Height);
                        //Нижняя линия
                        gl.Vertex(x, y, Line_Height);
                        gl.Vertex(x + fontsize / 2, y, Line_Height);
                        //Верхняя черта
                        gl.Vertex(x, y + fontsize / 2, Line_Height);
                        gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                        //Правая верхняя черта
                        gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                        gl.Vertex(x + fontsize / 2, y + fontsize / 4, Line_Height);
                        //Средняя черта
                        gl.Vertex(x, y + fontsize / 4, Line_Height);
                        gl.Vertex(x + fontsize / 2, y + fontsize / 4, Line_Height);
                    }
                    else
                    {
                        Enum_act(Actions.Средняя);
                        Enum_act(Actions.Верхняя);
                        Enum_act(Actions.Нижняя);
                        Enum_act(Actions.Левая_верт_полная);
                    }
                    X_draw_move();
                    break;
                case '#':
                    X_draw_move();
                    break;
                case 'a':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case 'b':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Б_верхняя);
                    Enum_act(Actions.Б_нижняя);
                    X_draw_move();
                    break;
                case 'c':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    X_draw_move();
                    break;
                case 'd':
                    Enum_act(Actions.Левая_верт_полная);
                    //Enum_act(Actions.Х_верхне_лев);
                    //Enum_act(Actions.Х_нижн_лев);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    gl.Vertex(x, y, Line_Height);
                    gl.Vertex(x, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    X_draw_move();
                    break;
                case 'f':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    X_draw_move();
                    break;
                case 'g':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Правая_верт_нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    X_draw_move();
                    break;
                case 'h':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case 'i':
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    //Снизу и сверху черточка
                    gl.Vertex(x + fontsize * 1 / 8, y, Line_Height);
                    gl.Vertex(x + fontsize * 3 / 8, y, Line_Height);
                    gl.Vertex(x + fontsize * 1 / 8, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize * 3 / 8, y + fontsize, Line_Height);
                    X_draw_move();
                    break;
                case 'j':
                    //Верхняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case 'k':
                    Enum_act(Actions.Левая_верт_полная);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    X_draw_move();
                    break;
                case 'l':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    X_draw_move();
                    break;
                case 'm':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    Enum_act(Actions.Х_верхне_лев);
                    Enum_act(Actions.Х_верхне_прав);
                    X_draw_move();
                    break;
                case 'n':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    gl.Vertex(x, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    X_draw_move();
                    break;
                case 'o':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case 'p':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_верхняя);
                    X_draw_move();
                    break;
                case 'q':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    Enum_act(Actions.Запятая);
                    //gl.Vertex(x + fontsize / 3, y - fontsize / 3, Line_Height);
                    //gl.Vertex(x + fontsize / 5, y, Line_Height);
                    X_draw_move();
                    break;
                case 'r':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Б_верхняя);
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    X_draw_move();
                    break;
                case 's':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_верхняя);
                    Enum_act(Actions.Правая_верт_нижняя);
                    X_draw_move();
                    break;
                case 't':
                    Enum_act(Actions.Верхняя);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    X_draw_move();
                    break;
                case 'u':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case 'v':
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    X_draw_move();
                    break;
                case 'w':
                    Enum_act(Actions.Х_нижн_лев);
                    Enum_act(Actions.Х_нижн_прав);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move();
                    break;
                case 'x':
                    Enum_act(Actions.Х_верхне_лев);
                    Enum_act(Actions.Х_верхне_прав);
                    Enum_act(Actions.Х_нижн_лев);
                    Enum_act(Actions.Х_нижн_прав);
                    X_draw_move();
                    break;
                case 'y':
                    Enum_act(Actions.Х_верхне_лев);
                    Enum_act(Actions.Х_верхне_прав);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    X_draw_move();
                    break;
                case 'z':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Наискосок_семерки);
                    X_draw_move();
                    break;
                case ' ':
                    X_draw_move();
                    break;
                case '_':
                    Enum_act(Actions.Нижняя);
                    X_draw_move();
                    break;
                case '=':
                    gl.Vertex(x, y + fontsize * 3 / 8, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize * 3 / 8, Line_Height);
                    gl.Vertex(x, y + fontsize * 5 / 8, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize * 5 / 8, Line_Height);
                    X_draw_move();
                    break;
                case '\t':
                    X_draw_move();
                    X_draw_move();
                    X_draw_move();
                    X_draw_move();
                    break;
                case '\'':
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize * 6 / 8, Line_Height);
                    X_draw_move();
                    break;
                case 'а': Translating_Swither('a'); break;
                case 'б':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_нижняя);
                    X_draw_move(); break;
                case 'в': Translating_Swither('b'); break;
                case 'г':
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    X_draw_move(); break;
                case 'д':
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Левая_верт_нижняя);
                    Enum_act(Actions.Правая_верт_нижняя);
                    X_draw_move();
                    break;
                case 'е':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    X_draw_move(); break;
                case 'ё':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    gl.Vertex(x + fontsize * 2 / 16, y + fontsize * 9 / 8, Line_Height);
                    gl.Vertex(x + fontsize * 3 / 16, y + fontsize * 9 / 8, Line_Height);
                    gl.Vertex(x + fontsize * 5 / 16, y + fontsize * 9 / 8, Line_Height);
                    gl.Vertex(x + fontsize * 6 / 16, y + fontsize * 9 / 8, Line_Height);
                    X_draw_move(); break;
                case 'ж':
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    Translating_Swither('x');
                    break;
                case 'з':
                    Enum_act(Actions.Б_верхняя);
                    Enum_act(Actions.Б_нижняя);
                    X_draw_move(); break;
                case 'и':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    //Палки И
                    gl.Vertex(x, y, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    X_draw_move(); break;
                case 'й':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    //Палки И
                    gl.Vertex(x, y, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    //Палки Й
                    gl.Vertex(x + fontsize * 1 / 8, y + fontsize * 9 / 8, Line_Height);
                    gl.Vertex(x + fontsize * 3 / 8, y + fontsize * 9 / 8, Line_Height);
                    X_draw_move(); break;
                case 'к': Translating_Swither('k'); break;
                case 'л':
                    gl.Vertex(x, y, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    X_draw_move(); break;
                case 'м': Translating_Swither('m'); break;
                case 'н': Translating_Swither('h'); break;
                case 'о': Translating_Swither('o'); break;
                case 'п':
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move(); break;
                case 'р':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Б_верхняя);
                    X_draw_move(); break;
                case 'с': Translating_Swither('c'); break;
                case 'т': Translating_Swither('t'); break;
                case 'у':
                    Enum_act(Actions.Х_верхне_лев);
                    Enum_act(Actions.Х_верхне_прав);
                    Enum_act(Actions.Х_нижн_лев);
                    X_draw_move(); break;
                case 'ф':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Верхняя);
                    Enum_act(Actions.Левая_верт_верхняя);
                    Enum_act(Actions.Правая_верт_верхняя);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    X_draw_move(); break;
                case 'х': Translating_Swither('x'); break;
                case 'ш':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    X_draw_move(); break;
                case 'ц':
                    //Щ запятая
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    gl.Vertex(x + fontsize / 4, y - fontsize / 8, Line_Height);
                    Translating_Swither('u'); break;
                case 'ч':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Левая_верт_верхняя);
                    Enum_act(Actions.Правая_верт_полная);
                    X_draw_move(); break;
                case 'щ':
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    //Щ запятая
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    gl.Vertex(x + fontsize / 4, y - fontsize / 8, Line_Height);
                    X_draw_move(); break;
                case 'ь':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_нижняя);
                    X_draw_move(); break;
                case 'ы':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize * 2 / 8, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize * 2 / 8, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize * 2 / 8, y, Line_Height);
                    gl.Vertex(x + fontsize * 2 / 8, y, Line_Height);
                    gl.Vertex(x, y, Line_Height);
                    X_draw_move(); break;
                case 'ъ':
                    Enum_act(Actions.Средняя);
                    Enum_act(Actions.Нижняя);
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_нижняя);
                    //Ъ черта
                    gl.Vertex(x, y + fontsize, Line_Height);
                    gl.Vertex(x - fontsize / 8, y + fontsize, Line_Height);
                    X_draw_move(); break;
                case 'э':
                    Enum_act(Actions.Средняя);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    gl.Vertex(x, y, Line_Height);
                    gl.Vertex(x, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    X_draw_move(); break;
                case 'ю':
                    Enum_act(Actions.Левая_верт_полная);
                    Enum_act(Actions.Правая_верт_полная);
                    //Средняя вертикально нижняя черта
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    ////////////////////////////////////////////////
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize, Line_Height);
                    gl.Vertex(x + fontsize / 4, y, Line_Height);
                    gl.Vertex(x + fontsize / 2, y, Line_Height);
                    gl.Vertex(x + fontsize / 4, y + fontsize / 2, Line_Height);
                    gl.Vertex(x, y + fontsize / 2, Line_Height);
                    X_draw_move(); break;
                case 'я':
                    Enum_act(Actions.Правая_верт_полная);
                    gl.Vertex(x, y, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize / 2, Line_Height);
                    gl.Vertex(x, y + fontsize * 3 / 4, Line_Height);
                    gl.Vertex(x, y + fontsize * 3 / 4, Line_Height);
                    gl.Vertex(x + fontsize / 2, y + fontsize, Line_Height);
                    X_draw_move(); break;


                default:
                    //gl.Vertex(x, y, Line_Height);
                    //gl.Vertex(x+ (fontsize / 2), y+ fontsize, Line_Height);
                    X_draw_move();
                    break;
            }
        }
        bool Bool_its_number = false;
        public CalculatorFont(OpenGLControl openGLControl)
        {
            gl = openGLControl.OpenGL;
        }
        //Мысленно здесь класс функций калькуляторного шрифта заканчивается
        public void Ultimate_DrawText(int _x, int _y, System.Drawing.Color _colour, float _fontsize, string phrase, float LineWidth, OpenGLControl center_aligned = null)
        {
            //openGLControl.OpenGL.DrawText(x, y, r, g, b, "TimesNewRoman", fontsize, phrase);

            Bool_its_number = Its_number(phrase[0]);

            x = _x; y = _y;
            
            //  Clear the color and depth buffer.
            //  Load the identity matrix.
            gl.LoadIdentity();
            gl.Color((float)_colour.R/255, (float)_colour.G / 255, (float)_colour.B / 255, 1.0f); //Must have, weirdness!
            gl.LineWidth(LineWidth);
            gl.Begin(OpenGL.GL_LINES);

            fontsize = _fontsize;

            phrase = phrase.ToLower();
            static_step = fontsize * 8 / 10;

            if (center_aligned != null)
            {
                x = (int)(((double)center_aligned.Width / 2) - ((double)phrase.Length * static_step / 2));
            }

            //fontsize
            foreach (var symbol in phrase)
            {
                Translating_Swither(symbol);
                //gl.Color(0.0f, 0.0f, 1.0f);
                //gl.Vertex(x_from, y_from, Line_Height);
                //gl.Color(0.0f, 0.0f, 1.0f);
                //gl.Vertex(x_to, y_to, Line_Height);
            }
            gl.End();
        }
    }
}
