using Delete.Helpers;
using Database;
using Patients;
namespace Delete.Patients
{
    public static class DeletePatient
    {
public static void Delete(Patient patient)
        {
            FormulationDelete.Delete();
            
            using(PatientDBContext db = new PatientDBContext())
            {
                db.Patient.Remove(patient);
                db.SaveChanges();
            }

        }
    }
}
