namespace passaro1;

public partial class MainPage : ContentPage
{
	const int gravidade = 3;
	const int tempoEntreFrames = 25;
	bool morto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 9;
	const int maxTempoPulando =3;
	int tempoPulando = 0;
	bool estaPulando = false;
	const int forcaPulo =60;


	public MainPage()
	{
		InitializeComponent();
	}

	async Task Desenhar()
	{
		while (!morto)
		{
			if (estaPulando)
				AplicarPulo();
			else 
				AplicarGravidade();
			await Task.Delay(tempoEntreFrames);
			GerenciaCanos();
			if (VerificaColisao())
			{
				morto = true;
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}
	void AplicarGravidade()
	{
		passaro.TranslationY += gravidade;
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
		if (canodebaixo.TranslationX < -larguraJanela)
		{
			canodebaixo.TranslationX = 100;
			canodecima.TranslationX = 100;
		}
	}
	void Inicializar()
	{
		morto = false;
		passaro.TranslationY = 0;
	}

	void OnGameOverClicked(object s, TappedEventArgs a)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		Desenhar();

	}

	bool VerificaColisao()
	{
		if (!morto)
		{
			if (VerificaColisaoTeto() ||
				VerificaColisaoChao())
			{
				return true;
			}
				
		}
		return false;
	}
	 private bool VerificaColisaoTeto()
	{
		var minY =-alturaJanela/2;
		if (passaro.TranslationY <=minY)

	 	    return true;
	    else
			return false;
	}

	bool VerificaColisaoChao()
	{
		var mixY = alturaJanela/2; 
		if (passaro.TranslationY >=mixY)
	 		return true;
	 else
			return false;
	}
	void AplicarPulo()
	{
		passaro.TranslationY -=forcaPulo;
		tempoPulando ++;
		if (tempoPulando >=maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}
	}
	void OnGridClickd(object s,TappedEventArgs a)
	{
		estaPulando = true;
	}

}