using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital
{

    //<!Summary>
    //Abstract class person having person information
    //Having concrete method Display
    abstract class Person
    {

        public string _firstName;
        public string _lastName;
        public string _email;
        public string _role;
        public Person(string firstName, string lastName, string email, string role)
        {
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _role = role;
        }

        public void Display()
        {
            Console.WriteLine($" Name : {this._firstName}  {this._lastName}");
            Console.WriteLine($"Email : {this._email}");
            Console.WriteLine($"Role : {this._role}");
        }

    }

    //<!Summary>
    //Concrete class Doctor include doctor additional information
    //Having concrete method Display
    class Doctor : Person
    {
        public int _doctorId;
        public string _specialization;


        public Doctor(int doctorId, string firstName, string lastName, string email, string specialization)
            : base(firstName, lastName, email, "Doctor")
        {
            _doctorId = doctorId;
            _specialization = specialization;
        }


        //<!Summary>
        //Deconstruct method
        public void Deconstruct(out int doctorId, out string fName, out string lName, out string email, out string spec)
        {
            doctorId = _doctorId;
            fName = _firstName;
            lName = _lastName;
            email = _email;
            spec = _specialization;


        }
        public void Display()
        {
            Console.WriteLine($"Patient id {this._doctorId}");
            base.Display();
            Console.WriteLine($"Specialization : {this._specialization}");
        }


        public int GetID()
        {
            return this._doctorId;

        }
        public string GetName()
        {
            return $"{this._firstName} {this._lastName}";

        }
    }

    //<!Summary>
    //Concrete class Patient include patient additional information
    //Having concrete method Display
    class Patient : Person
    {
        int _patientId;
        string _diagnose;

        public Patient(int patientId, string firstName, string lastName, string email, string diagnose)
                        : base(firstName, lastName, email, "Patient")
        {
            _patientId = patientId;
            _diagnose = diagnose;
        }


        public int GetID()
        {
            return this._patientId;

        }
        public string GetName()
        {
            return $"{this._firstName} {this._lastName}";

        }
        public void Display()
        {
            Console.WriteLine($"Patient id {this._patientId}");
            base.Display();
            Console.WriteLine($"Diagnose {this._diagnose}");
        }

    }

    //<!Summary>
    //Concrete class Appointment include appointment information
    //Having method Display
    class Appointment
    {
        int _appointmentId;
        Doctor _doctor;

        Patient _patient { get; set; }
        DateTime _date { get; set; }
        int _startHour;
        int _endHour;
        int _startMinute;
        int _endMinute;
        public string Doctor
        {
            get
            {
                return $"{_doctor._firstName}  {_doctor._lastName}";
            }
        }
        public string Date
        {
            get
            {
                return $"{_date}";
            }
        }


        public Appointment(int id, Doctor doctor, Patient patient, DateTime date)
        {
            _appointmentId = id;
            _doctor = doctor;
            _patient = patient;
            _date = date;
            _startHour = date.Hour;
            _startMinute = date.Minute;
            _endMinute = date.Minute + 30;
            _endHour = date.Hour;
            if (_endMinute >= 60)
            {
                _endMinute %= 60;
                _endHour++;

            }

        }

        public void Display()
        {
            Console.WriteLine($"Appointment id {this._appointmentId}");
            Console.WriteLine($"Appointment Doctor : {this._doctor.GetName()}");
            Console.WriteLine($"Appointment Patient : {this._patient.GetName()}");
            Console.WriteLine($"Appointment Starts On : {this._date.Day}");
            Console.WriteLine($"Appointment Starts At : {this._startHour} {this._startMinute}");
            Console.WriteLine($"Appointment Ends At : {this._endHour} {this._endMinute}");
        }


    }

    //<!Summary>
    //Concrete class SendingEmail
    //responsible for Notifying doctor and patient for appointment details

    interface IService
    {
        public void Send();
    }
    class SendingEmail : IService
    {

        public void Send()
        {


            Console.WriteLine("Sending Email");

        }
    }
    class SendingSMS : IService
    {

        public void Send()
        {
            Console.WriteLine("Sending SMS");

        }
    }


    class NotifyService
    {
        IService _service;
        public void SetService( IService service )
        { _service = service; }
        public void Notify()
        {
            _service.Send();

        }
        
    }


    //<!Summary>
    //Concrete class Hospital Having informations about patients and Doctors belong to it
    //Adding Doctor Method 
    //Adding Patient Method
    //Displaying all data in it
    class Hospital
    {
        string _name;
        string _address;
        private List<Doctor> doctors;
        List<Patient> _patients;
        List<Appointment> _appointments;

        internal List<Doctor> Doctors { get => doctors; set => doctors = value; }

        public Hospital(string name, string address)
        {

            _name = name;
            _address = address;
            doctors = new List<Doctor>();
            _patients = new List<Patient>();
            _appointments = new List<Appointment>();
        }


        public void Display()
        {
            Console.WriteLine($"Hospital Name : {this._name}");
            Console.WriteLine($"Hospital Address : {this._address}");
            Console.WriteLine("Doctors are : ");
            foreach (Doctor Doct in this.doctors)
            {
                Console.WriteLine($"{Doct.GetName()}");
            }
            Console.WriteLine("Patients are : ");
            foreach (Patient patient in this._patients)
            {
                Console.WriteLine($"{patient.GetName()}");
            }
        }
        public void AddDoctor(Doctor Doct)
        {
            doctors.Add(Doct);
        }
        public void AddPatient(Patient Patient)
        {
            _patients.Add(Patient);
        }
    }
   public enum services { SMS, Email };
    class ServiceFactory
    {
        public static IService Create(services type)
        {
            switch(type)
            {
                case services.SMS:
                    return new SendingSMS();
                case services.Email:
                    return new SendingEmail();
                default:
                    return new SendingEmail();


            }
        }
    }

    //Here is the Code goes
    class Program
    {
        static void Main(string[] args)
        {
            //intializing id for each class
            int docId = 1;
            int patientId = 1;
            int appointmentId = 1;


            //intializing object from hospital
            Hospital hospital = new Hospital("ElSalam", "Mohandessien");




            //intializing object from Doctor
            Doctor islam = new Doctor(docId++, "Islam", "yasser", "islamYasser@gmail.com", "Surgey");
            islam.Display();
            Console.WriteLine("------------------------------");


            ////intializing object from Patient
            Patient Islam = new Patient(patientId++, "Islam", "yasser", "islamYasser@gmail.com", "bones");
            Islam.Display();
            Console.WriteLine("------------------------------");


            //intializing object from appointment
            DateTime date =new DateTime();
            Appointment appointmentOne = new Appointment(appointmentId++, islam, Islam, date);


            //Sending Notification
            IService _service = ServiceFactory.Create(services.Email);
            NotifyService _notifyService = new NotifyService(); 
            _notifyService.SetService( _service );
            _notifyService.Notify();
            Console.WriteLine("------------------------------");

            
            //intializing objects from Doctors
             Doctor islam1 = new Doctor(docId++, "Islam1", "yasser", "islamYasser@gmail.com", "Allergy and Immunology");
             Doctor islam2 = new Doctor(docId++, "Islam2", "yasser", "islamYasser@gmail.com", "Anesthesiology");
             Doctor islam3 = new Doctor(docId++, "Islam3", "yasser", "islamYasser@gmail.com", "Cardiology");
             Doctor islam4 = new Doctor(docId++, "Islam4", "yasser", "islamYasser@gmail.com", "Dermatology");
            

            //adding doctors to hospital array
            hospital.AddDoctor(islam1);
            hospital.AddDoctor(islam2);
            hospital.AddDoctor(islam3);
            hospital.AddDoctor(islam4);
            


            //adding patient to hospital array
            hospital.AddPatient(Islam);
            

            //Display Hospital Details
            hospital.Display();
            Console.WriteLine("------------------------------");

            //Display Hospital Details
            appointmentOne.Display();
            Console.WriteLine("------------------------------");



            //<!Summary>
            //Discard and Deconstruct
            (var doctorId, var fName, _, _, var spec) = islam;
            Console.WriteLine(doctorId);
            Console.WriteLine(fName);
            Console.WriteLine(spec);
            Console.WriteLine("---------------------------------");



            //<!Summary>
            //Pattern Matching 
            Random random = new Random(); 
            var randomDoc = hospital.Doctors[(int)(random.Next())% hospital.Doctors.Count];
            var DocSpecia = randomDoc switch
             {
                 { _specialization : "Cardiology" } =>$"doctor is : Cardiology and Name is :{randomDoc._firstName + " "+ randomDoc._lastName}",
                 { _specialization : "Surgey" } =>$"doctor is : Surgey and Name is :{randomDoc._firstName + " " + randomDoc._lastName}",
                 { _specialization : "Dermatology" } =>$"doctor is : Dermatology and Name is :{randomDoc._firstName + " " + randomDoc._lastName}",
                 { _specialization : "Anesthesiology" } =>$"doctor is : Anesthesiology and Name is :{randomDoc._firstName + " " + randomDoc._lastName}",
                 { _specialization : "Allergy and Immunology" } =>$"doctor is : Allergy and Immunology and Name is :{randomDoc._firstName + " " + randomDoc._lastName}",
                 _ =>$"doctor is : Default",

             };
             Console.WriteLine(DocSpecia);
        }
    }

}
