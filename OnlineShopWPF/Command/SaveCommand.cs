using System.Windows;

namespace OnlineShopWPF.Command
{
    public class SaveCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            MessageBox.Show("Сохранение выполнено");
        }
    }
}
