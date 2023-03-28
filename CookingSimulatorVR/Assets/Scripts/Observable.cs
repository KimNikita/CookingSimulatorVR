using static GlobalVariables;

// Интерфейс, который нужно имплементировать классу, извещающему о наступлении события
// Позволяет уведомлять подписчиков-обозревателей о каком-либо событии
public interface Observable
{
    // добавить подписчиков в список (список должен быть в классе, имплементирующем интерфейс)
    void AddObserver(Observer o);
    // удалить подписчиков из списка (список должен быть в классе, имплементирующем интерфейс)
    void RemoveObserver(Observer o);
    // в этом методе для всех подписчиков в списке должен вызываться метод Observer::HandleEvent(achievements)
    void NotifyObserver(achievements ach);
}
