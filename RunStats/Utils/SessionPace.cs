using Microsoft.AspNetCore.Mvc;
using RunStats.Models;

namespace RunStats.Utils
{
    public class SessionPace
    {
        public static string CalculatePace(RunningSession? session)
        {
            if (session == null) return "Brak sesji";
            if (!session.Time.HasValue) return "Sesja trwa";
            try
            {
                double tempoInMinutesPerKm = (double)session.Time / (session.Distance / 1000.0) / 60000.0;

                // Oblicz minuty i sekundy
                int minutes = (int)Math.Floor(tempoInMinutesPerKm);
                int seconds = (int)Math.Round((tempoInMinutesPerKm - minutes) * 60);

                // Formatuj czas w postaci "min:sek min/km"
                string paceString = $"{minutes}:{seconds:D2} min/km";

                return paceString;
            }
            catch (Exception ex)
            {
                throw new Exception($"Wystąpił błąd: {ex.Message}");
            }
        }

    }
}
