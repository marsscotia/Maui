using CommunityToolkit.Maui.Sample.Models;
using CommunityToolkit.Maui.Sample.Pages.Alerts;

namespace CommunityToolkit.Maui.Sample.ViewModels.Alerts;

public class AlertsGalleryViewModel : BaseGalleryViewModel
{
	public AlertsGalleryViewModel(SnackbarPage snackbarPage, ToastPage toastPage)
		: base(new[]
		{
			new SectionModel(snackbarPage, "Snackbar", "Show Snackbar"),
			new SectionModel(toastPage, "Toast", "Show Toast")
		})
	{
	}
}