namespace GlownaAplikacja
{
    #region Usings
    using System.Windows.Forms;
    using NAudio.CoreAudioApi;
    using System.Linq;
    #endregion

    public partial class Options : Form
    {
        #region Fields
        private MMDevice device;
        private int speed;
        #endregion

        #region Public Properties
        public MMDevice Device { get => device; set => device = value; }
        public int Speed { get => speed; set => speed = value; }
        #endregion

        #region Constructors and Deconstructors
        public Options()
        {
            InitializeComponent();
            loadAudioDevices();
        }
        #endregion

        #region Methods
        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (cbAudio.SelectedIndex > -1)
                Device = (MMDevice)cbAudio.SelectedItem;
            if (!string.IsNullOrEmpty(txtSpeed.Text))
            {
                Speed = int.Parse(txtSpeed.Text);
            }
        }

        private void loadAudioDevices()
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            cbAudio.Items.AddRange(devices.ToArray());
        }
        #endregion

        private void txtSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz wyjść? Wszystkie zmiany zostaną anulowane.", "Ostrzeżenie", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.Close();
        }
    }
}
