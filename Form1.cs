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

            DateTime minDate = DateTime.Now;
            DateTime maxDate = minDate.AddYears(5);

            errorMessage += IsPresent(txtArrivalDate.Text, "Arrival date");
            errorMessage += IsPresent(txtDepartureDate.Text, "Departure date");

            errorMessage += IsDateTime(txtArrivalDate.Text, "Arrival date");
            errorMessage += IsDateTime(txtDepartureDate.Text, "Departure date");

            errorMessage += IsWithinDateRange(txtArrivalDate.Text, "Arrival date", minDate, maxDate);
            errorMessage += IsWithinDateRange(txtDepartureDate.Text, "Departure date", minDate, maxDate);

            errorMessage += IsLaterDate(txtArrivalDate.Text, "Arrival date", txtDepartureDate.Text, "Departure date");

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

        // check whether the input format is in date format
        public string IsDateTime(string value, string name)
        {
            DateTime date;
            if (!DateTime.TryParse(value, out date))
            {
                return name + " must be a valid date.\n";
            }
            return "";
        }

        public string IsWithinDateRange(string value, string name, DateTime min, DateTime max)
        {
            DateTime date;
            if (!DateTime.TryParse(value, out date))
            {
                return name + " must be a valid date.\n";
            }
            if (date < min || date > max)
            {
                return name + " must be between " + min.ToShortDateString() + " and " + max.ToShortDateString() + ".\n";
            }
            return "";
        }


        public string IsLaterDate(string earlyValue, string earlyName, string laterValue, string laterName)
        {
            DateTime earlyDate, laterDate;
            if (!DateTime.TryParse(earlyValue, out earlyDate) || !DateTime.TryParse(laterValue, out laterDate))
            {
                return earlyName + " and " + laterName + " must be valid dates.\n";
            }
            if (laterDate <= earlyDate)
            {
                return laterName + " must be later than " + earlyName + ".\n";
            }
            return "";
        }

        // form load event


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // button click event
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (!IsValidData())
            {
                return;
            }

            decimal priceWeekday = 120m;
            decimal priceFriSat = 150m;

            DateTime arrivalDate = DateTime.Parse(txtArrivalDate.Text);
            DateTime departureDate = DateTime.Parse(txtDepartureDate.Text);

            TimeSpan dateDiff = departureDate.Subtract(arrivalDate);
            double numOfNights = dateDiff.TotalDays;
            txtNights.Text = numOfNights.ToString();

            DateTime currentDate = arrivalDate;
            decimal totalPrice = 0m;

            while (currentDate < departureDate)
            {
                if (currentDate.DayOfWeek != DayOfWeek.Friday && currentDate.DayOfWeek != DayOfWeek.Saturday)
                {
                    totalPrice += priceWeekday;
                }
                else
                {
                    totalPrice += priceFriSat;
                }
                currentDate = currentDate.AddDays(1);
            }

            decimal pricePerNight = totalPrice / (decimal)numOfNights;

            txtTotalPrice.Text = totalPrice.ToString("c");
            txtAvgPrice.Text = pricePerNight.ToString("c");
        }
          
        
            
        

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now; // today's date
            DateTime threeDays = today.AddDays(3); // 3 days later

            txtArrivalDate.Text = today.ToString("yyyy-MM-dd");
            txtDepartureDate.Text = threeDays.ToString("yyyy-MM-dd");
        }
    }
}