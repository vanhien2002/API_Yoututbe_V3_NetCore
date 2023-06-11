namespace BlueTube.NETCore.Models
{
    public class Helper
    {
        public static string ViewCount(int _viewCount)
        {
            if (_viewCount < 9000)
            {
                return _viewCount.ToString();
            }
            else if (_viewCount <= 9000000)
            {
                //99.999 => 1.000
                return (_viewCount / 1000).ToString() + "N";
            }
            else if (_viewCount <= 90000000)
            {
                float n = (float)_viewCount / 1000000;
                string kq = Math.Round(n, 1).ToString();
                return kq + "tr";
            }
            else if (_viewCount <= 900000000)
            {
                float n = (float)_viewCount / 10000000;
                string kq = Math.Round(n, 1).ToString();
                return kq + "tr";
            }
            return _viewCount.ToString();
        }
        public static string ViewUploadDate (string date)
        {
            DateTime currentDate = DateTime.Now.Date;
            string resutl;
            string uploadDate = "2022-01-31T00:00:00+00:00";
            string currentYear = currentDate.Year.ToString();
            string currentMonth = currentDate.Month.ToString();
            string[] listitem = uploadDate.Split('-');
            if (listitem[0].Equals(currentYear) == true)
            {
                //trùng năm
                int dem = int.Parse(currentMonth) - int.Parse(listitem[1]);
                resutl = dem.ToString() + " tháng trước";
            }
            else
            {
                //không trung năm
                int dem = int.Parse(currentYear) - int.Parse(listitem[0]);
                resutl = dem.ToString() + " năm trước";
            }
            return resutl;
        }
    }
}
