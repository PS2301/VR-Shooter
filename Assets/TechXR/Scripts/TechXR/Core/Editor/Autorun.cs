using UnityEngine;
using UnityEditor;

namespace TechXR.Core.Editor
{
    [InitializeOnLoad]
    public class Autorun
    {
        private static System.DateTime startDate;
        private static System.DateTime today;
        private static System.DateTime setDate;

        static Autorun()
        {
            EditorApplication.update += RunOnce;
        }

        static void RunOnce()
        {
            SetStartDate();

            double daysPassed = GetDaysPassed();
            if (daysPassed >= 90.0)
            {
                //FileUtil.DeleteFileOrDirectory("Assets/StreamingAssets");
            }

            double daysRemaining = GetRemainingDays();
            if (daysRemaining <= 0.0)
            {
                //FileUtil.DeleteFileOrDirectory("Assets/StreamingAssets");
            }

            EditorApplication.update -= RunOnce;
        }

        static void SetStartDate()
        {
            if (PlayerPrefs.HasKey("DateInitialized")) //if we have the start date saved, we'll use that
                startDate = System.Convert.ToDateTime(PlayerPrefs.GetString("DateInitialized"));
            else //otherwise...
            {
                startDate = System.DateTime.Now; //save the start date ->
                PlayerPrefs.SetString("DateInitialized", startDate.ToString());
            }
        }

        static double GetDaysPassed()
        {
            today = System.DateTime.Now;

            //days between today and start date -->
            System.TimeSpan elapsed = today.Subtract(startDate);

            double days = elapsed.TotalDays;

            //Debug.Log("No. of Days Passed ----> " + days);

            return days;
        }

        static double GetRemainingDays()
        {
            var dt = System.DateTime.Now;

            var targetDate = new System.DateTime(dt.Year, 04, 30, 0, 0, 0);

            //Debug.Log("Now -> " + System.DateTime.Now + " Target ->" + targetDate + "Total :=> " + System.Math.Abs((targetDate - dt).TotalDays));

            return System.Math.Abs((targetDate - dt).TotalDays);
        }
    }
}