using SmartBudget.ClassLibrary;

namespace SmartBudget
{
    // Абстрактный класс для локализации
    public abstract class LocalizedUserControl : UserControl
    {
        protected virtual void ApplyLocalization()
        {
            // Переопределяется в каждом классе
        }

        public void UpdateLocalization()
        {
            // Загружаем язык из настроек
            SettingsService settings = SettingsService.LoadSettings();
            if (settings != null && !string.IsNullOrEmpty(settings.Language))
                LocalizationManager.SetLanguage(settings.Language);
            ApplyLocalization();
        }
    }
}