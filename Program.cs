using System;
using MedicalResearchApp;
using SampleConApp;
using Database;

namespace MedicalResearchApp
{
    class Disease
    {
        public string DiseaseName { get; set; }
        public string Severity { get; set; }
        public string Cause { get; set; }
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public string PatientName { get; set; }

        public void Shallowcopy(Disease copy)
        {
            this.DiseaseName = copy.DiseaseName;
            this.Severity = copy.Severity;
            this.Cause = copy.Cause;
            this.Description = copy.Description;
            this.Symptoms = copy.Symptoms;
            this.PatientName = copy.PatientName;
        }

        public Disease Deepcopy(Disease copy)
        {
            Disease disease = new Disease();
            disease.Shallowcopy(copy);
            return disease;
        }

    }

}

namespace Database
{
    class DiseaseDatabase
    {
        private Disease[] _disease = null;
        private readonly int _size = 0;
        public DiseaseDatabase(int size)
        {
            _size = size;
            _disease = new Disease[_size];
        }

        public int AddDiseaseToSystem(Disease disease)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_disease[i] == null)
                {
                    _disease[i] = disease.Deepcopy(disease);
                    return 1;
                }
            }
            return _size;
        }

        public int AddSymptoms(Disease disease)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_disease[i] == null)
                {
                    _disease[i] = disease.Deepcopy(disease);
                    return 1;
                }
            }
            return _size;
        }

        public int CheckPatient(Disease disease)
        {
            for (int i = 0; i < _size; i++)
            {
                if (_disease[i] == null)
                {
                    _disease[i] = disease.Deepcopy(disease);
                    return 1;
                }
            }
            return _size;
        }

        //public Disease[] FindBySymptoms(string symptoms)
        //{
        //    int count = 0;
        //    foreach (Disease diseases in _disease)
        //    {
        //        if (diseases != null && diseases.Symptoms.Contains(symptoms))
        //        {
        //            count += 1;
        //        }
        //    }
        //    Disease[] disease = new Disease[count];
        //    count = 0;
        //    foreach (Disease diseases in _disease)
        //    {
        //        if (diseases != null && diseases.Symptoms.Contains(symptoms))
        //        {
        //            disease[count] = diseases.Deepcopy(diseases);
        //            count += 1;
        //        }
        //    }
        //    return disease;
        //}
    }
}

namespace UILayer
{
    enum Options
    {
        AddDiseaseToSystem = 1,
        AddSymptomToDisease = 2,
        CheckPatient = 3,
        FindBySymptoms = 4
    }

    class UI
    {
        public const string Menu = "~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~MEDICAL RESEARCH APP~~~~~~~~~~~~~~~~~~~\nTO ADD DISEASE TO THE SYSTEM------------------------>PRESS 1\nTO ADD SYMPTOM TO THE DISEASE----------------------->PRESS 2\nCHECK PATIENT--------------------------------------->PRESS 3\nFIND DISEASE FROM SYMPTOMS-------------------------->PRESS 4\nPS: ANY OTHER KEY IS CONSIDERED AS EXIT.....................................";

        private static DiseaseDatabase repo;

        public static void Test()
        {
            int size = Utilities.GetNumber("Enter the Number of Reports you want to fetch:");
            repo = new DiseaseDatabase(size);

            bool process = true;
            do {
                Options option = (Options)Utilities.GetNumber(Menu);
                process = AllMenu(option);
            } while (process);
            Console.WriteLine("Be Healthy!");
        }
        private static bool AllMenu(Options option)
        {
            switch (option)
            {
                case Options.AddDiseaseToSystem:
                    AddNewDisease();
                    break;
                case Options.AddSymptomToDisease:
                    AddSymptoms();
                    break;
                case Options.CheckPatient:
                    CheckPatient();
                    break;
                //case Options.FindBySymptoms:
                //    findingDiseaseBySymptoms();
                //    break;
                default:
                    return false;
            }
            return true;
        }

        private static void AddNewDisease()
        {
            string diseasename = Utilities.Prompt("Enter Name of the Disease:");
            string severity = Utilities.Prompt("Severity:");
            string cause = Utilities.Prompt("Cause:");
            string description = Utilities.Prompt("Enter Description:");
            Disease disease = new Disease { DiseaseName = diseasename, Severity = severity, Cause = cause, Description = description };
            repo.AddDiseaseToSystem(disease);
            Utilities.Prompt("\nFill Symptoms");

        }

        private static void AddSymptoms()
        {
            string disease_name = Utilities.Prompt("Enter Name of the Disease:");
            string symptoms = Utilities.Prompt("Enter Symptoms:");
            string description = Utilities.Prompt("Fill Description:");
            Disease disease = new Disease { DiseaseName = disease_name, Symptoms = symptoms, Description = description };
            repo.AddSymptoms(disease);
            Utilities.Prompt("Fill Patient Details");
        }

        private static void CheckPatient()
        {
            string patient_name = Utilities.Prompt("Enter Name of the Diseased Patient:");
            string symptoms_name = Utilities.Prompt("Symptoms Name:");
            string description = Utilities.Prompt("Fill Description:");
            Disease disease = new Disease { PatientName = patient_name, Symptoms = symptoms_name, Description = description };
            repo.CheckPatient(disease);
            Utilities.Prompt("Exit");
            Console.Clear();
        }

        //private static void findingDiseaseBySymptoms()
        //{
        //    string symptoms = Utilities.Prompt("Enter the Symptoms of the Disease to Find:");
        //    try
        //    {
        //        Disease[] details = repo.FindBySymptoms(symptoms);
        //        Console.WriteLine("The Details of the Disease are as follows:");
        //        foreach (var item in details)
        //        {
        //            string content = $"The Symptoms: {item.Symptoms}\nThe Disease: {item.DiseaseName}\n";
        //            Console.WriteLine(content);
        //        }

        //        Utilities.Prompt("Press Enter to clear the Screen");
        //        Console.Clear();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
    }
}

namespace MedicalConsole
{
    using MedicalResearchApp;
    using SampleConApp;
    using System;

    class App
    {
        static void Main(string[] args)
        {
            UILayer.UI.Test();
        }
    }
}

