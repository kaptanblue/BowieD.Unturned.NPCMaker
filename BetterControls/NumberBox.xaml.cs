﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Media;

namespace BowieD.Unturned.NPCMaker.BetterControls
{
    /// <summary>
    /// Логика взаимодействия для NumberBox.xaml
    /// </summary>
    public partial class NumberBox : UserControl
    {
        public NumberBox()
        {
            InitializeComponent();
        }
        
        public long MinValue
        {
            get
            {
                return (long)GetValue(MinValueProperty);
            }
            set
            {
                SetValue(MinValueProperty, value);
            }
        }
        public long MaxValue
        {
            get
            {
                return (long)GetValue(MaxValueProperty);
            }
            set
            {
                SetValue(MaxValueProperty, value);
            }
        }
        public long Value
        {
            get
            {
                Value = long.TryParse(mainBox.Text, out long result) ? result : 0;
                return (long)GetValue(ValueProperty);
            }
            set
            {
                mainBox.Text = value.ToString();
                SetValue(ValueProperty, value);
            }
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(long), typeof(NumberBox), new PropertyMetadata((long)0));
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(long), typeof(NumberBox), new PropertyMetadata(long.MaxValue));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(long), typeof(NumberBox), new PropertyMetadata((long)0));
        
        private bool IsTextAllowed(string text)
        {
            if (long.TryParse(text, out long result))
            {
                if (result < MinValue)
                {
                    SystemSounds.Beep.Play();
                    return false;
                }
                if (result > MaxValue)
                {
                    SystemSounds.Beep.Play();
                    return false;
                }
                if (text.Length > (MaxValue.ToString().Length > MinValue.ToString().Length ? MaxValue.ToString().Length : MinValue.ToString().Length))
                {
                    SystemSounds.Beep.Play();
                    return false;
                }
                return true;
            }
            SystemSounds.Beep.Play();
            return false;
        }

        private void MainBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = (sender as TextBox).Text + e.Text;
            e.Handled = !IsTextAllowed(text);
        }

        private void MainBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsTextAllowed(text))
                {
                    e.CancelCommand();
                    return;
                }
                //Value = long.Parse(text);
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}