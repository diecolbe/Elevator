using com.device.Interfaces;

namespace com.device
{
    public partial class ElevatorFrm : Form, IElevator
    {
        private int selectedFloor;
        private List<Floor> floors;
        private int? ActualFloor = 1;       

        public ElevatorFrm()
        {
            InitializeComponent();
            floors = new List<Floor>()
            {
                new Floor()
                {
                    NumberFloor = 1,
                    x = 359,
                    y = 1057
                },
                new Floor()
                {
                    NumberFloor = 2,
                    x = 359,
                    y = 825
                },
                new Floor()
                {
                    NumberFloor = 3,
                    x = 359,
                    y = 594
                },
                new Floor()
                {
                    NumberFloor = 4,
                    x = 359,
                    y = 350
                },
                new Floor()
                {
                    NumberFloor = 5,
                    x = 359,
                    y = 76
                }
            };
        }

        public void btn_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Name.Equals("buttonOpen"))
            {
                OpenDoor();
            }
            else if (button.Name.Equals("buttonClose"))
            {
                CloseDoor();
            }
            else if (button.Name.Equals("buttonUp")
                || button.Name.Equals("buttonDown"))
            {
                //OpenOrCloseDoor();
            }
            else if (button.Name.Equals("buttonfloor1") ||
                button.Name.Equals("buttonfloor2") ||
                button.Name.Equals("buttonfloor3") ||
                button.Name.Equals("buttonfloor4") ||
                button.Name.Equals("buttonfloor5"))
            {
                selectedFloor = Convert.ToInt32(button.Text);
                OnElevator();
            }
        }

        private void Elevator_Load(object sender, EventArgs e)
        {
            foreach (Control c in Controls)
            {
                if (c is Button)
                {
                    c.Click += new System.EventHandler(btn_click);
                }
            }
        }

        public void OpenDoor()
        {
            pictureBoxElevatorOpen.Visible = true;
            pictureBoxElevatorClose.Visible = false;
        }

        public void CloseDoor()
        {
            pictureBoxElevatorClose.Visible = true;
            pictureBoxElevatorOpen.Visible = false; ;
        }

        public void OnElevator()
        {
            var floor = floors.FirstOrDefault(f => f.NumberFloor == selectedFloor);

            if (floor != null)
            {
                if (ActualFloor < floor.NumberFloor)
                    UpElevator(floor);
                else
                    DownElevator(floor);
            }
        }

        public void UpElevator(Floor floor)
        {
            SuspendLayout();
            Floor? actualFloor = floors.FirstOrDefault(f => f.NumberFloor == ActualFloor);
            var totalFloors = floors.Where(f => f.NumberFloor <= floor.NumberFloor).ToList();

            for (int y = actualFloor.y; y >= floor.y; y--)
            {
                var currentFloor = totalFloors.FirstOrDefault(f => f.y == y);
                if (currentFloor != null)
                {
                    textBox_ActualFloor.Text = $"Piso: {currentFloor.NumberFloor}";
                }
                Thread.Sleep(10);
                Application.DoEvents();
                pictureBoxElevatorClose.Location = new Point(floor.x, y);
                pictureBoxElevatorOpen.Location = new Point(floor.x, y);
                
            }
            pictureBoxElevatorOpen.Visible = true;
            pictureBoxElevatorClose.Visible = false;
            textBox_ActualFloor.Text = $"Bienvenido al piso: {floor.NumberFloor}";
            ActualFloor = floor.NumberFloor;
            ResumeLayout();
        }

        public void DownElevator(Floor floor)
        {
            SuspendLayout();
            Floor? actualFloor = floors.FirstOrDefault(f => f.NumberFloor == ActualFloor);
            var totalFloors = floors.Where(f => f.NumberFloor >= floor.NumberFloor);
            for (int y = actualFloor.y; y <= floor.y; y++)
            {
                var currentFloor = totalFloors.FirstOrDefault(f => f.y == y);
                if (currentFloor != null)
                {
                    textBox_ActualFloor.Text = $"Piso: {currentFloor.NumberFloor}";
                }
                Thread.Sleep(10);
                Application.DoEvents();
                //When you use Thread.Sleep, always remember to call Application.DoEvents() 
                pictureBoxElevatorClose.Location = new Point(floor.x, y);
                pictureBoxElevatorOpen.Location = new Point(floor.x, y);
            }

            pictureBoxElevatorOpen.Visible = true;
            pictureBoxElevatorClose.Visible = false;
            textBox_ActualFloor.Text = $"Bienvenido al piso: {floor.NumberFloor}";
            ActualFloor = floor.NumberFloor;
            ResumeLayout();

        }

        public void StopElevator()
        {
            timerElevator.Enabled = false;
        }

        private void timerElevator_Tick(object sender, EventArgs e)
        {
            //OnElevator();
        }
    }
}