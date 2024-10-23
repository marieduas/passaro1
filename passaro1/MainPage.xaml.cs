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
	const int aberturaMinima = 200;
	int score = 0;




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
			score++;
			if (score%2==0)
			    velocidade++;
			labelLP.Text = "canos: "+score.ToString("D3");
			var alturaMax =-100;
			var alturaMin =-canodebaixo.HeightRequest;
			canodecima.TranslationY = Random.Shared.Next((int)alturaMin,(int)alturaMax);
			canodebaixo.TranslationY = canodecima.TranslationY + aberturaMinima + canodebaixo.HeightRequest;
		}
	}
	void Inicializar()
	{
		morto = false;
		passaro.TranslationY = 0;
		passaro.TranslationX = 0;
		score = 0;
		GerenciaCanos();
		canodebaixo.TranslationY =-larguraJanela;
		canodecima.TranslationX =-larguraJanela;
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
			return VerificaColisaoTeto() ||
				VerificaColisaoChao() ||
				VerificaColisaoCanoCima()||
				VerificaColisaoCanoBaixo();
				
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
	bool VerificaColisaoCanoCima()
	{
		var posHpassaro = (larguraJanela/2)-(passaro.WidthRequest/2);
		var posVpassaro = (alturaJanela/2)-(passaro.HeightRequest/2)+passaro.TranslationY;
		if (posHpassaro>= Math.Abs(canodecima.TranslationX)-canodecima.WidthRequest &&
		    posHpassaro<= Math.Abs(canodecima.TranslationX)+canodecima.WidthRequest &&
			posVpassaro<= canodecima.HeightRequest +canodecima.TranslationY)
			{
				return true;
			}
			else
			{
				return false;
			}

	}
	bool VerificaColisaoCanoBaixo()
	{
		var posHpassaro = (larguraJanela/2)-(passaro.WidthRequest/2);
		var posVpassaro = (alturaJanela/2)-(passaro.HeightRequest/2)+passaro.TranslationY;
		var yMaxCano = canodecima.HeightRequest + canodecima.TranslationY + aberturaMinima;
		if (posHpassaro>= Math.Abs(canodebaixo.TranslationX)-canodebaixo.WidthRequest &&
		    posHpassaro<= Math.Abs(canodebaixo.TranslationX)+canodebaixo.WidthRequest && 
			posVpassaro>=yMaxCano)
			{
				return true;
			}
			else
			{
				return false;
			}
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