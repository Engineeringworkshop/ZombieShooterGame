using UnityEngine;
using Pablo.CharacterStats;

public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private CharacterStat[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();

        // Actualizamos los nombres de los stats
        UpdateStatNames();
    }

    // Metodo para recibir los stats
    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        // Si hemos recibido más stats de los displays que tenemos => error
        if (stats.Length > statDisplays.Length)
        {
            Debug.LogError("Not enough stat dispays!");
            return;
        }

        // Si recibimos menos stats que displays => desactivamos los stats extra
        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length); // si i es menor que el numero de stats, activalo, si no, desactivalo

            if (i < stats.Length)
            {
                statDisplays[i].Stat = stats[i];
            }
        }

    }

    // Metodo para cambiar los valores de los stats en el UI
    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].UpdateStatValue();
        }
    }

    // metodo para cambiar los nombres de los stats
    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].Name = statNames[i];
        }
    }
}
