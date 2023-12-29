namespace RunStats.Utils
{
    public class RoundToNearestHour
    {
        public static DateTime round(DateTime dateTime)
        {
            int minutes = dateTime.Minute;
            int roundedMinutes = (int)Math.Round((double)minutes / 60) * 60;
            if (roundedMinutes == 60)
            {
                dateTime = dateTime.AddHours(1).AddMinutes(-minutes); // Jeśli zaokrąglone minuty wynoszą 60, dodaj godzinę i ustaw minuty na 0
            }
            else
            {
                dateTime = dateTime.AddMinutes(roundedMinutes - minutes); // W przeciwnym razie dodaj różnicę do minuty
            }

            return dateTime;
        }

    }
}
