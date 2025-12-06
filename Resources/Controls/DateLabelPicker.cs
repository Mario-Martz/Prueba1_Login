using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomControls
{
    public class DateLabelPicker : UserControl
    {
        private Label label;
        private DateTimePicker datePicker;

        private int spacing = 15;   // separación por defecto (más amplia que antes)

        [Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string LabelText
        {
            get => label.Text;
            set => label.Text = value;
        }

        [Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public DateTime Value
        {
            get => datePicker.Value;
            set => datePicker.Value = value;
        }

        [Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool LabelVisible
        {
            get => label.Visible;
            set => label.Visible = value;
        }

        [Category("Custom")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool DatePickerVisible
        {
            get => datePicker.Visible;
            set => datePicker.Visible = value;
        }

        // ----------- NUEVA PROPIEDAD -----------

        [Category("Layout")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Spacing
        {
            get => spacing;
            set
            {
                spacing = value;
                UpdateLayout();
            }
        }

        // ---------------------------------------

        public DateLabelPicker()
        {
            this.Height = 40;
            this.Width = 260;

            label = new Label()
            {
                Text = "Título",
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(0, 10)
            };

            datePicker = new DateTimePicker()
            {
                Format = DateTimePickerFormat.Short,
                Font = new Font("Segoe UI", 10F),
                Width = 110,
                Location = new Point(label.Width + spacing, 6)
            };

            Controls.Add(label);
            Controls.Add(datePicker);

            UpdateLayout();
        }

        private void UpdateLayout()
        {
            datePicker.Location = new Point(label.Width + spacing, 6);
        }
    }
}