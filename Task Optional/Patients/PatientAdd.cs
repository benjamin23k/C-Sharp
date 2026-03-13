using Database;
using Patients;

namespace Add.Patients
{
    public static class AddPatient
    {
        public static void Add(Patient patient)
        {
            using (PatientDBContext db = new PatientDBContext())
            {
                db.Patient.Add(patient);
                db.SaveChanges();
            }
        }
    }
}


