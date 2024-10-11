namespace passaro1;

public partial class MainPage : ContentPage
{
	const int gravidade = 2; 
	const int tempoEntreFrames = 25;
	bool estaMorto = true;
	int count = 0;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 7;

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
			GerenciaCanos();
		}
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenhar();
	}
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
		larguraJanela = width;
		alturaJanela = height;
    }
    void GerenciaCanos()
	{
		canodecima.TranslationX -= velocidade;
		canodebaixo.TranslationX -= velocidade;
		if (canodebaixo.TranslationX <- larguraJanela)
		{
			canodebaixo.TranslationX = 0;
			canodecima.TranslationX = 0;
		}
	}
	void OnGameOverClicked (object s,TappedEventArgs args)
	{
		iniciar.IsVisible = false;
		Inicializar();
		Desenhar();
	}

	void Inicializar()
	{
		estaMorto = false;
		iniciar.TranslationY = 0;
	}




}

