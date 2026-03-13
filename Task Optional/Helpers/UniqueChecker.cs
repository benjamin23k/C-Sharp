using Database;
using Patients;

namespace Helpers
{
    public static class UniqueChecker
    {
        public static bool IsPersonalIdUnique(string personalId, int? excludePatientId = null)
        {
            using var db = new PatientDBContext();
            var query = db.Patient.Where(p => p.Personal == personalId);
            if (excludePatientId.HasValue)
            {
                query = query.Where(p => p.Id != excludePatientId.Value);
            }
            return !query.Any();
        }
    }
}
