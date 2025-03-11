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

        // check whether the input format is in date format
        public string IsDateTime(string value, string name)
        {
            DateTime date = new DateTime();
            string msg = "";
            if (!DateTime.TryParse(value, out date))
            {
                msg = name + "must be in date form";
            }

            return msg;
        }

        public string IsWithinDateRange(string value, string name,
            DateTime min, DateTime max)
        {
            string msg = "";
            if (DateTime.Parse(value)<min || DateTime.Parse(value)> max)
            {
                msg = "Date not within range " + min + " and " + max;
            }
            return msg;
        }

        public string IsLaterDate(string earlyValue, string earlyName,
            string laterValue, string laterName)
        {
            string msg = earlyName + " must be before " + laterName;

            return msg;
        }

        // form load event


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // button click event
        private void btnCalculate_Click(object sender, EventArgs e)
        {
                  
            decimal priceWeekday = 120m;
            decimal priceFriSat = 150m;

            // create arrival and depature datetime objects
            DateTime arrivalDate;
            DateTime departureDate;

            // try and parse both inputs into datetime values
            bool arrivalCheck = DateTime.TryParse(txtArrivalDate.Text, out arrivalDate);
            bool departureCheck = DateTime.TryParse(txtDepartureDate.Text, out departureDate);

            // if they cannot be parsed into datetime values then error messages are thrown
            if(!arrivalCheck)
            {
                string msg = IsDateTime(txtArrivalDate.Text, "Arrival Date");
                MessageBox.Show("Arrival date " + msg);
            } 
            else if (!departureCheck)
            {
                string msg = IsDateTime(txtDepartureDate.Text, "Departure Date");
                MessageBox.Show(msg);
            } 
            else if (DateTime.Parse(txtDepartureDate.Text) <= DateTime.Parse(txtArrivalDate.Text))
            {
                string msg = IsLaterDate(txtArrivalDate.Text,"Arrival Date", txtDepartureDate.Text, "Departure Date");
                MessageBox.Show(msg);
            }
            else
            {
                TimeSpan dateDiff = departureDate.Subtract(arrivalDate); //create TimeSpan variable, subtracting arrival from departure

                double numOfNights = dateDiff.TotalDays; // pull only the difference of days from dateDiff. 
                txtNights.Text = numOfNights.ToString(); // set the number of days to the text box value

                DateTime currentDate = arrivalDate; // set currentDate = arrivalDate for while loop condition
                decimal totalPrice = 0m; // totalPrice = 0 and gets increased based on while loop

                // Calculate total price based on the day of the week
                while (currentDate < departureDate)
                {
                    // apply weekday pricing if not Friday or Saturday
                    if (currentDate.DayOfWeek != DayOfWeek.Friday && currentDate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        totalPrice += priceWeekday; // add weekday price to total price for every weekday
                    }
                    else
                    {
                        totalPrice += priceFriSat; // add weekend price to totalfor every weekend day
                    }
                    currentDate = currentDate.AddDays(1); // iterate the day  
                }

                decimal pricePerNight = totalPrice / (decimal)numOfNights; // calculate the price per night

                // display the values in their respective text boxes
                txtTotalPrice.Text = totalPrice.ToString("c");
                txtAvgPrice.Text = pricePerNight.ToString("c");
            }
          
        }
            
        

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now; // today's date
            DateTime threeDays = today.AddDays(3); // 3 days later

            txtArrivalDate.Text = today.ToString("yyyy-MM-dd");
            txtDepartureDate.Text = threeDays.ToString("yyy-MM-dd");
        }
    }
}