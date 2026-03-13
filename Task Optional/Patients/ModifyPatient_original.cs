using Modify.Helpers;
using Database;
using Patients;

namespace Modify.Patients
{
    public static class ModifyPatient
    {
        public static void Modify(Patient patient)
        {
            FormulationModify.ModifyPatient();

               using (PatientDBContext db = new PatientDBContext())
            {
                db.Patient.Update(patient);
                db.SaveChanges();
            }    
        
        }
    }
}
