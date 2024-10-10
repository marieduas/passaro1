namespace passaro1;

public partial class MainPage : ContentPage
{
	const int gravidade = 2; 
	const int tempoEntreFrames = 25;
	bool estaMorto = false;
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}
	void AplicaGravidade()
	{
		passaro.TranslationY += gravidade;
	}
	async Task Desenhar()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(tempoEntreFrames);
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenhar();
	}





}

