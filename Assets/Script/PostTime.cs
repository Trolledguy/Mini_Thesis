using UnityEngine;

public struct PostTime
{
    public int day;
    public int month;
    public int year;

    public PostTime(int day, int month, int year)
    {
        this.day = day;
        this.month = month;
        this.year = year;
    }
    public string GetFormattedDate()
    {
        switch (month)
        {
            case 1: return $"{day:D2} January {year}";
            case 2: return $"{day:D2} February {year}";
            case 3: return $"{day:D2} March {year}";
            case 4: return $"{day:D2} April {year}";
            case 5: return $"{day:D2} May {year}";
            case 6: return $"{day:D2} June {year}";
            case 7: return $"{day:D2} July {year}";
            case 8: return $"{day:D2} August {year}";
            case 9: return $"{day:D2} September {year}";
            case 10: return $"{day:D2} October {year}";
            case 11: return $"{day:D2} November {year}";
            case 12: return $"{day:D2} December {year}";
            default: return $"{day:D2}/{month:D2}/{year}";
        }   
    }
}