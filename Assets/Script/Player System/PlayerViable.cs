using UnityEngine;

public class PlayerViable
{
    [Header("Player Settings")]
    public string playerName;
    private int m_playerEnergy;
    private int m_moneyBalance;
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
    

    public PlayerViable(ViableSetting viableSetting)
    {
        playerName = "DefaultPlayer";
        m_playerEnergy = viableSetting.playerEnergy;
        m_moneyBalance = viableSetting.moneyBalance;
        m_timeRemainingPerDay = viableSetting.timeRemainingPerDay;
    }



} 