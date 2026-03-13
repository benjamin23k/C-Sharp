using Formulationseach.Helpers;
using Patients;
using Database;

namespace Search.Patients
{
    public static class SearchPatient
    {
        public static void Search(Patient patient)
        {
            FormulationSearch.search();
            using (PatientDBContext db = new PatientDBContext())
            {
                db.Patient.Find(patient.Id);
            }
        }
    }
}
