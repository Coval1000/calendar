using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public partial class Calendar : Control
    {
        public enum Centred { None, Horizontal, Vertical, Centred };

        [TypeConverter(typeof(GridBgConventer))]
        public class GridBg
        {
            private int _left;
            private int _right;
            private int _top;
            private int _bottom;
            private int _horizontal;
            private int _vertical;
            private int _thick;
            private Centred _centred;


            public Centred centred
            {
                get { return _centred; }
                set
                {
                    if (_centred != value)
                    {
                        _centred = value;
                        switch (_centred)
                        {
                            case Centred.Horizontal:
                                _horizontal = (_left + _right) / 2;
                                break;

                            case Centred.Vertical:
                                _vertical = (_top + _bottom) / 2;
                                break;

                            case Centred.Centred:
                                _thick = (_top + _bottom + _left + _bottom) / 4;
                                break;
                        }
                    }
                }
            }

            public int left
            {
                get { return _left; }
                set
                {
                    if (value >= 0)
                        _left = value;
                }
            }
            public int right
            {
                get { return _right; }
                set
                {
                    if (value >= 0)
                        _right = value;
                }
            }
            public int top
            {
                get { return _top; }
                set
                {
                    if (value >= 0)
                        _top = value;
                }
            }
            public int bottom
            {
                get { return _bottom; }
                set
                {
                    if (value >= 0)
                        _bottom = value;
                }
            }

            public int vertical
            {
                get { return _vertical; }
                set
                {
                    if (value >= 0)
                        _vertical = value;
                }
            }
            public int horizontal
            {
                get { return _horizontal; }
                set
                {
                    if (value >= 0)
                        _horizontal = value;
                }
            }
            public int thick
            {
                get { return _thick; }
                set
                {
                    if (value >= 0)
                        _thick = value;
                }
            }


            public GridBg()
            { }

            public GridBg(int uniformLength)
            {
                this.left = uniformLength;
                this.right = uniformLength;
            }

            public GridBg(int Lc, int Rc)
            {
                this.left = Lc;
                this.right = Rc;
            }

            public GridBg(int Lc, int Rc, int Tp, int Bp, Centred c)
            {
                this.left = Lc;
                this.right = Rc;
                this.bottom = Bp;
                this.top = Tp;
                this.centred = c;
            }

            public GridBg(int Lc, int Rc, int Tp, int Bp)
            {
                this.left = Lc;
                this.right = Rc;
                this.bottom = Bp;
                this.top = Tp;
            }

            public GridBg(int Lc = 0, int Rc = 0, int Tp = 0, int Bp = 0, int v = 0, int h = 0, int t = 0, Centred c = Centred.None)
            {
                this.left = Lc;
                this.right = Rc;
                this.bottom = Bp;
                this.top = Tp;
                this.horizontal = h;
                this.vertical = v;
                this.thick = t;
                this.centred = c;
            }

            public GridBg(int t, Centred c)
            {
                this.thick = t;
                this.centred = c;
            }

            public GridBg(int LH, int RB, int VT, Centred c)
            {
                switch(c)
                {
                    case Centred.Horizontal:
                        this.horizontal = LH;
                        this.bottom = RB;
                        this.top = VT;
                        this.centred = c;
                        break;
                    case Centred.Vertical:
                        this.left = LH;
                        this.right = RB;
                        this.vertical = VT;
                        this.centred = c;
                        break;
                }
            }
        }

        public class GridBgConventer : TypeConverter
        {
            public GridBgConventer()
            {
            }

            public override Boolean CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(String) || destinationType == typeof(InstanceDescriptor);
            }
            public override Object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value, Type destinationType)
            {
                GridBg thickness = (GridBg)value;
                switch (thickness.centred)
                {
                    case Centred.None:
                        if (destinationType == typeof(String))
                        {
                            return thickness.left + "," + thickness.right + "," + thickness.top + "," + thickness.bottom + "," + thickness.centred;
                        }
                        else if (destinationType == typeof(InstanceDescriptor))
                        {
                            {
                                ConstructorInfo constructor = typeof(GridBg).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(Centred) });
                                return new InstanceDescriptor(constructor, new Object[] { thickness.left, thickness.right, thickness.top, thickness.bottom, thickness.centred });
                            }
                        }
                        break;
                    case Centred.Vertical:
                        if (destinationType == typeof(String))
                        {
                            return thickness.left + "," + thickness.right + "," + thickness.vertical + "," + thickness.centred;
                        }
                        else if (destinationType == typeof(InstanceDescriptor))
                        {
                            {
                                ConstructorInfo constructor = typeof(GridBg).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int),  typeof(Centred) });
                                return new InstanceDescriptor(constructor, new Object[] { thickness.left, thickness.right, thickness.vertical, thickness.centred });
                            }
                        }
                        break;
                    case Centred.Horizontal:
                        if (destinationType == typeof(String))
                        {
                            return thickness.horizontal + "," + thickness.top + "," + thickness.bottom + "," + thickness.centred;
                        }
                        else if (destinationType == typeof(InstanceDescriptor))
                        {
                            {
                                ConstructorInfo constructor = typeof(GridBg).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(Centred) });
                                return new InstanceDescriptor(constructor, new Object[] { thickness.horizontal , thickness.top , thickness.bottom, thickness.centred });
                            }
                        }
                        break;
                    case Centred.Centred:
                        if (destinationType == typeof(String))
                        {
                            return thickness.thick + "," + thickness.centred;
                        }
                        else if (destinationType == typeof(InstanceDescriptor))
                        {
                            {
                                ConstructorInfo constructor = typeof(GridBg).GetConstructor(new Type[] { typeof(int), typeof(Centred) });
                                return new InstanceDescriptor(constructor, new Object[] { thickness.thick, thickness.centred });
                            }
                        }
                        break;
                }
                    return null;
                    
            }
            public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(String);
            }
            public override Object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value)
            {
                if (value is String)
                {
                    String stringValue = (String)value;
                    if (stringValue.Contains(","))
                    {
                        String[] stringValues = stringValue.Split(',');
                        int[] values = new int[stringValues.Length];
                        try
                        {
                            for (Int32 i = 0; i < 7; i++)
                            {
                                values[i] = int.Parse(stringValues[i]);
                            }
                        }
                        catch (Exception)
                        {
                            return new GridBg();
                        }
                        return new GridBg(values[0], values[1], values[2], values[3], values[4], values[5], values[6]);
                    }
                    else return new GridBg();
                }
                else return base.ConvertFrom(context, culture, value);
            }
            public override Boolean GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override Object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
            {
                return new GridBg((int)propertyValues["left"], (int)propertyValues["right"], (int)propertyValues["top"], (int)propertyValues["bottom"], (int)propertyValues["vertical"], (int)propertyValues["horizontal"], (int)propertyValues["thick"], (Centred)propertyValues["centred"]);
            }

            public override Boolean GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(GridBg));
                collection = collection.Sort(new String[] { "left", "right" ,"top", "bottom", "horizontal", "vertical", "thick", "centred" });

                return collection;
            }
        }

        [TypeConverter(typeof(LukaSiatkiConventer))]
        public class LukaSiatki
        {
            private int _gapY;
            private int _gapX;
            private int _posX;
            private int _posY;
            private Centred _centred;


            public Centred centred
            {
                get { return _centred; }
                set
                {
                    _centred = value;
                    switch((int)_centred)
                    {
                        case 1:
                            _posX = gapX / 2;
                            break;
                        case 2:
                            _posY = gapY / 2;
                            break;
                        case 3:
                            _posX = gapX / 2;
                            _posY = gapY / 2;
                            break;
                    }
                }
            }

            public int gapX
            {
                get { return _gapX; }
                set
                {
                    if (value != _gapX && value >= 0)
                    {
                        _gapX = value;
                        if ((int)centred == 1 || (int)centred == 3) _posX = gapX / 2;
                    }
                }
            }
            public int gapY
            {
                get { return _gapY; }
                set
                {
                    if (value != _gapY && value >= 0)
                    {
                        _gapY = value;
                        if ((int)centred == 2 || (int)centred == 3) _posY = gapY / 2;
                    }
                }
            }
            public int posX
            {
                get { return _posX; }
                set
                {
                    if (value != _posX && value >= 0 && value <= gapX && ((int)centred == 0 || (int)centred == 2))
                    {
                        _posX = value;
                    }
                }
            }
            public int posY
            {
                get { return _posY; }
                set
                {
                    if (value != _posY && value >= 0 && value <= gapY && ((int)centred == 0 || (int)centred == 1))
                    {
                        _posY = value;
                    }
                }
            }



            public LukaSiatki()
            { }

            public LukaSiatki(int uniformLength)
            {
                this.gapX = uniformLength;
                this.gapY = uniformLength;
            }

            public LukaSiatki(int Xc, int Yc)
            {
                this.gapX = Xc;
                this.gapY = Yc;
            }

            public LukaSiatki(int Xc, int Yc, int Xp, int Yp, Centred c)
            {
                this.gapX = Xc;
                this.gapY = Yc;
                this.posX = Xp;
                this.posY = Yp;
                this.centred = c;
            }

            public LukaSiatki(int Xc, int Yc, int Xp, int Yp)
            {
                this.gapX = Xc;
                this.gapY = Yc;

                this.posX = Xp;
                this.posY = Yp;
            }
        }

        public class LukaSiatkiConventer : TypeConverter
        {
            public LukaSiatkiConventer()
            {
            }

            public override Boolean CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return destinationType == typeof(String) || destinationType == typeof(InstanceDescriptor);
            }
            public override Object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value, Type destinationType)
            {
                LukaSiatki thickness = (LukaSiatki)value;
                if (destinationType == typeof(String))
                {
                    return thickness.gapX + "," + thickness.gapY + "," + thickness.posX + "," + thickness.posY + "," + thickness.centred;
                }
                else if (destinationType == typeof(InstanceDescriptor))
                {
                    {
                        ConstructorInfo constructor = typeof(LukaSiatki).GetConstructor(new Type[] { typeof(int), typeof(int),typeof(int), typeof(int), typeof(Centred)});
                        return new InstanceDescriptor(constructor, new Object[] { thickness.gapX, thickness.gapY, thickness.posX, thickness.posY , thickness.centred });
                    }
                }
                else return null;
            }
            public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return sourceType == typeof(String);
            }
            public override Object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, Object value)
            {
                if (value is String)
                {
                    String stringValue = (String)value;
                    if (stringValue.Contains(","))
                    {
                        String[] stringValues = stringValue.Split(',');
                        int[] values = new int[stringValues.Length];
                        try
                        {
                            for (Int32 i = 0; i < 4; i++)
                            {
                                    values[i] = int.Parse(stringValues[i]);
                            }
                        }
                        catch (Exception)
                        {
                            return new LukaSiatki();
                        }
                        return new LukaSiatki(values[0], values[1], values[2], values[3]);
                    }
                    else return new LukaSiatki();
                }
                else return base.ConvertFrom(context, culture, value);
            }
            public override Boolean GetCreateInstanceSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override Object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
            {
                return new LukaSiatki((int)propertyValues["gapX"], (int)propertyValues["gapY"], (int)propertyValues["posX"], (int)propertyValues["posY"], (Centred)propertyValues["centred"]);
            }

            public override Boolean GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return true;
            }
            public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, Object value, Attribute[] attributes)
            {
                PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(LukaSiatki));
                collection = collection.Sort(new String[] { "gapX", "gapY", "posX", "posY", "centred" });
                //collection.RemoveAt(2);
                //collection.RemoveAt(2);
                return collection;
            }
        }

        public Calendar()
        {
            imgB_prawy = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\prawy.png");
            imgB_lewy = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\lewy.png");
            d_frame_normal = Image.FromFile("C:\\\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\d_frame_normal.png");
            d_frame_active = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\d_frame_active.png");
            d_frame_today = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\d_frame_today.png");
            d_bg_normal = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\d_bg_normal.png");
            d_accent_normal = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\d_accent_normal.png");
            d_accent_sunday = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\d_accent_sunday.png");
            siatka_bg = Image.FromFile("C:\\Users\\User\\Source\\Repos\\calendar\\WindowsFormsApplication1\\WindowsFormsApplication1\\sprite\\bg.png");


            gap = new LukaSiatki();
            grid = new GridBg();
            today = DateTime.Now;
            active = today;
            InitializeComponent();
            panel = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width, panelHigh);
            monthName = new Font("Verdana", panel.Height * 0.5f + 1, FontStyle.Bold, GraphicsUnit.Pixel);
        }

        #region Properties
        private GridBg _grid;
        private LukaSiatki _gap;
        private int _panelHigh;
        private Image _imgB_prawy;
        private Image _imgB_lewy;
        private int _left;
        private int _right;
        private int _top;
        private int _bottom;

        private int _leftInner;
        private int _rightInner;
        private int _topInner;
        private int _bottomInner;

        private Font _monthName;

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Day grid"),
            Browsable(true)]
        public GridBg grid
        {
            get { return _grid; }
            set
            {
                _grid = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Day grid"),
            Browsable(true)]
        public LukaSiatki gap
        {
            get { return _gap; }
            set
            {
                _gap = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public Font monthName
        {
            get { return _monthName; }
            set { _monthName = value; Invalidate(); }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int right
        {
            get { return _right; }
            set
            {
                if (value >= 0) _right = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int top
        {
            get { return _top; }
            set
            {
                if (value >= 0) _top = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int bottom
        {
            get { return _bottom; }
            set
            {
                if (value >= 0) _bottom = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int left
        {
            get { return _left; }
            set
            {
                if (value >= 0) _left = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int rightInner
        {
            get { return _rightInner; }
            set
            {
                if (value >= 0) _rightInner = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int topInner
        {
            get { return _topInner; }
            set
            {
                if (value >= 0) _topInner = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int bottomInner
        {
            get { return _bottomInner; }
            set
            {
                if (value >= 0) _bottomInner = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int leftInner
        {
            get { return _leftInner; }
            set
            {
                if (value >= 0) _leftInner = value;
                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public Image imgB_lewy
        {
            get { return _imgB_lewy; }
            set
            {
                _imgB_lewy = value;

                Invalidate();
            }
        }

        [PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public Image imgB_prawy
        {
            get { return _imgB_prawy; }
            set
            {
                _imgB_prawy = value;

                Invalidate();
            }
        }

        [   PropertyTab("Panel"),
            Description("Szczelina"),
            Category("Panel")]
        public int panelHigh
        {
            get { return _panelHigh; }
            set
            {
                if (value != _panelHigh && value >= 0)
                {
                    _panelHigh = value;

                    Invalidate();
                }
            }
        }
        #endregion

        #region Fields
        Rectangle rectB_lewy;
        Rectangle rectB_prawy;


        private DateTime today;
        public DateTime active;

        private Rectangle element;
        private Rectangle siatka;
        private Rectangle panel;

        Image d_frame_normal;
        Image d_frame_active;
        Image d_frame_today;
        Image d_bg_normal;
        Image d_accent_normal;
        Image d_accent_sunday;
        Image siatka_bg;

        private bool _pressed;
        private bool pressed
        {
            get { return _pressed; }
            set
            {
                if (_pressed == value) return;
                _pressed = value;
            }
        }

        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (value == base.Text) return;
                base.Text = value;

                Invalidate();
            }
        }

        #endregion

        #region own methods
        public string month(DateTime t)
        {
            switch (t.Month)
            {
                case 1:
                    return "Styczeń";

                case 2:
                    return "Luty";

                case 3:
                    return "Marzec";

                case 4:
                    return "Kwiecień";

                case 5:
                    return "Maj";

                case 6:
                    return "Czerwiec";

                case 7:
                    return "Lipiec";

                case 8:
                    return "Sierpień";

                case 9:
                    return "Wrzesień";

                case 10:
                    return "Październik";

                case 11:
                    return "Listopad";

                case 12:
                    return "Grudzień";
            }
            return "<NaN>";
        }
        public string day(DateTime t)
        {
            switch ((int)t.DayOfWeek)
            {
                case 1:
                    return "Poniedziałek";

                case 2:
                    return "Wtorek";

                case 3:
                    return "Środa";

                case 4:
                    return "Czwartek";

                case 5:
                    return "Piątek";

                case 6:
                    return "Sobota";

                case 0:
                    return "Niedziela";
            }
            return "<NaN>";
        }
        public int daysOfMonth(int month, int year)
        {
            switch (month)
            {
                case 2:
                    if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                        return 29;
                    else
                        return 28;

                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;

                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
            }
            return 0;
        }
        public int beginOfMonth(DateTime t) //UWAGA!!! '0' to niedziela
        {
            int i = 1 + ((int)t.DayOfWeek - t.Day % 7);
            return i < 0 ? i+7 : i%7;
        }
        #endregion

        protected override void OnPaint(PaintEventArgs pe)
        {
            
            Graphics gfx = pe.Graphics;

            #region Panel
            panel = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width, panelHigh);
            rectB_lewy = new Rectangle(0, (panel.Height / 2) - 16, (int)(panel.Height * 0.5f), (int)(panel.Height * 0.5f));
            rectB_prawy = new Rectangle(panel.Width - imgB_prawy.Width, (panel.Height / 2) - 16, (int)(panel.Height * 0.5f), (int)(panel.Height * 0.5f));

            //

            StringFormat sf1 = new StringFormat();
            sf1.Alignment = StringAlignment.Center;
            sf1.LineAlignment = StringAlignment.Center;
            //do klasy
            gfx.FillRectangle(new SolidBrush(Parent.BackColor), panel);

            gfx.DrawImage(imgB_lewy, rectB_lewy);
            gfx.DrawImage(imgB_prawy, rectB_prawy);

            gfx.DrawString(month(active) + " " + active.Year.ToString(),
                      _monthName,
                      new SolidBrush(Color.Black),
                      new RectangleF((float)panel.Left, (float)panel.Top, (float)panel.Width, (float)panel.Height),
                      sf1);
            #endregion
            
            #region Siatka
            Image img_bg = (Image)siatka_bg.Clone();
            Graphics bg = Graphics.FromImage(img_bg);

            siatka = new Rectangle(ClientRectangle.Left + left, 
                ClientRectangle.Top + panelHigh + top, 
                ClientRectangle.Width - left - right, 
                ClientRectangle.Height - panelHigh - top - bottom);

            float pixel = (float)img_bg.Width / (float)siatka.Width;

            element = new Rectangle((int)(leftInner * pixel), 
                (int)(topInner * pixel), 
                img_bg.Width - (int)((leftInner + rightInner) * pixel), 
                img_bg.Height - (int)((topInner + bottomInner) * pixel));
            

            element.Width /= 7;
            element.Height /= 6;

            Rectangle dayField = element;
            RectangleF dayFx = new RectangleF((float)dayField.Left + gap.posX, (float)dayField.Top + gap.posY, (float)dayField.Width - gap.gapX, (float)dayField.Height - gap.gapY);
            //
            Font fn = new Font("Verdana", (float)Math.Sqrt((float)dayFx.Height * (float)dayFx.Height + (float)dayFx.Width * (float)dayFx.Width) * 0.35f, FontStyle.Bold, GraphicsUnit.Pixel);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            //do klasy
            int noofd1 = daysOfMonth(active.Month == 1 ? 12 : active.Month - 1, active.Year);
            int noofd2 = daysOfMonth(active.Month, active.Year);

            int index = beginOfMonth(active) <= 1 ? noofd1 - beginOfMonth(active) - 5 : noofd1 - beginOfMonth(active) + 2;
            bool trg = false;
            int monthPoss = -1;

            
            Color kolor = Color.Gray;
            Image tmp;

            for (int w = 1; w <= 6; w++)
            {
                dayFx.X = (float)dayField.Left + gap.posX;
                for (int d = 1; d <= 7; d++)
                {

                    bg.DrawImage(d_bg_normal, dayFx);
                    switch (monthPoss)
                    {
                        case -1:
                            switch (active.Month == 1 ? (beginOfMonth(active) + index - 1 - 31) % 7 : (beginOfMonth(active) + index - 1 - daysOfMonth(active.Month - 1, active.Year)) % 7)
                            {
                                case 0:
                                    tmp = d_accent_sunday;
                                    break;
                                default:
                                    tmp = d_accent_normal;
                                    break;
                            }
                            break;

                        case 0:
                            switch ((beginOfMonth(active) + index - 1) % 7)
                            {
                                case 0:
                                    tmp = d_accent_sunday;
                                    break;
                                default:
                                    tmp = d_accent_normal;
                                    break;
                            }
                            break;
                        default:
                            switch ((beginOfMonth(active) + index - 1 + daysOfMonth(active.Month, active.Year)) % 7)
                            {
                                case 0:
                                    tmp = d_accent_sunday;
                                    break;
                                default:
                                    tmp = d_accent_normal;
                                    break;
                            }
                            break;
                    }

                    bg.DrawImage(tmp, dayFx);


                    if (trg && today.Day == index && active.Month == today.Month && active.Year == today.Year)
                    {
                        tmp = d_frame_today;
                    }
                    else if (trg && active.Day == index)
                    {
                        tmp = d_frame_active;
                    }
                    else tmp = d_frame_normal;

                    bg.DrawImage(tmp, dayFx);

                    bg.DrawString(index.ToString(), fn, new SolidBrush(kolor), dayFx, sf);
                    dayFx.X += dayFx.Width + gap.gapX;
                    index++;
                    if (!trg && (index > noofd1))
                    {
                        index = 1;
                        trg = true;
                        kolor = Color.Black;
                        monthPoss++;
                    }
                    else if (trg && (index > noofd2))
                    {
                        index = 1;
                        kolor = Color.Gray;
                        trg = false;
                        monthPoss++;
                    }
                }
                dayFx.Y += dayFx.Height + gap.gapY;
            }
            gfx.DrawImage(img_bg, siatka);
            #endregion 

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !pressed)
            {
                pressed = true;
                if ((e.Y - panelHigh) > 0)
                {                    
                    int x = 7 * (e.X - siatka.X) / siatka.Width;
                    int y = 6 * (e.Y - siatka.Y) / siatka.Height;
                    active = beginOfMonth(active) <= 1 ?  active.AddDays((x + y * 7) - active.Day - beginOfMonth(active) - 5) : active.AddDays((x + y * 7) - active.Day - beginOfMonth(active) + 2);
                    Invalidate();
                }
                else
                {
                    if (e.X > rectB_lewy.X && e.X < (rectB_lewy.Width + rectB_lewy.X) && e.Y > rectB_lewy.Y && e.Y < (rectB_lewy.Height + rectB_lewy.Y))
                    {
                        active = active.AddDays(-daysOfMonth(active.Month, active.Year));
                        Invalidate();
                    }
                    else if (e.X > rectB_prawy.X && e.X < (rectB_prawy.Width + rectB_prawy.X) && e.Y > rectB_prawy.Y && e.Y < (rectB_prawy.Height + rectB_prawy.Y))
                    {
                        active = active.AddDays(daysOfMonth(active.Month, active.Year));
                        Invalidate();
                    }
                }
            }

        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && pressed) pressed = false;
        }
    }
}
