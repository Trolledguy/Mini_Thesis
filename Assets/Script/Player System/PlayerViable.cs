using UnityEngine;

public class PlayerViable
{
    [Header("Player Settings")]
    public string playerName;
    private int m_playerEnergy;
    private int m_moneyBalance;
    private int m_DayCounter = 1;
    private int[] m_daysFeedRequired; 
    private float m_timeRemainingPerDay;

    public int playerEnegy
    {
        get { return m_playerEnergy; }
        set 
        { 
            m_playerEnergy = value; 
            if(m_playerEnergy < 0)
            {
                m_playerEnergy = 0;
            }
        }
    }
    public int moneyBalance
    {
        get { return m_moneyBalance; }
        set 
        { 
            m_moneyBalance = value; 
            if(m_moneyBalance < 0)
            {
                m_moneyBalance = 0;
            }
        }
    }
    public float timeRemainingPerDay
    {
        get { return m_timeRemainingPerDay; }
        set 
        { 
            m_timeRemainingPerDay = value; 
            if(m_timeRemainingPerDay < 0)
            {
                m_timeRemainingPerDay = 0;
            }
        }
    }
    public int currentDay
    {
        get { return m_DayCounter; }
        set 
        { 
            m_DayCounter = value; 
            if(m_DayCounter < 1)
            {
                m_DayCounter = 1;
            }
        }
    }

    public int GetFeedRequired()
    {
        if (m_DayCounter - 1 < m_daysFeedRequired.Length)
        {
            return m_daysFeedRequired[m_DayCounter - 1];
        }
        else
        {
            Debug.LogWarning("Current day exceeds defined feed requirements. Returning last defined requirement.");
            return m_daysFeedRequired[m_daysFeedRequired.Length - 1];
        }
    }
    

    public PlayerViable(ViableSetting viableSetting)
    {
        playerName = "DefaultPlayer";
        m_playerEnergy = viableSetting.playerEnergy;
        m_moneyBalance = viableSetting.moneyBalance;
        m_timeRemainingPerDay = viableSetting.timeRemainingPerDay;
        m_daysFeedRequired = viableSetting.daysFeedRequired;
    }



} 