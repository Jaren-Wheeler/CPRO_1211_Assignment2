namespace Reservations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool IsValidData()
        {
            bool success = true;
            string errorMessage = "";

            // Validate the Arrival date text box
            errorMessage += IsPresent(txtArrivalDate.Text, "Arrival date");

            // Validate the Departure date text box
            errorMessage += IsPresent(txtDepartureDate.Text, "Departure date");

            if (errorMessage != "")
            {
                success = false;
                MessageBox.Show(errorMessage, "Entry Error");
            }
            return success;
        }

        public string IsPresent(string value, string name)
        {
            string msg = "";
            if (value == "")
            {
                msg = name + " is a required field.\n";
            }
            return msg;
        }

        public string IsDateTime(string value, string name)
        {
            string msg = "";

            return msg;
        }

        public string IsWithinDateRange(string value, string name,
            DateTime min, DateTime max)
        {
            string msg = "";

            return msg;
        }

        public string IsLaterDate(string earlyValue, string earlyName,
            string laterValue, string laterName)
        {
            string msg = "";

            return msg;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            decimal pricePerNight = 120m;

            // grab the two input values as strings and parse into DateTime variables
            DateTime arrivalDate = DateTime.Parse(txtArrivalDate.Text);
            DateTime departureDate = DateTime.Parse(txtDepartureDate.Text);

                        TimeSpan dateDiff = departureDate.Subtract(arrivalDate); //create TimeSpan variable, subtracting arrival from departure

            double days = dateDiff.TotalDays; // pull only the difference of days from dateDiff
            txtNights.Text = days.ToString(); // set the number of days to the text box value

            // Calculate total price
            decimal totalPrice = pricePerNight * (decimal) days;

            txtTotalPrice.Text = totalPrice.ToString("c");
            txtAvgPrice.Text = pricePerNight.ToString("c");
        }
    }
}