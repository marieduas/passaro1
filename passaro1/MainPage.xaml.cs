namespace passaro1;

public partial class MainPage : ContentPage
{
	const int gravidade = 20;
	const int tempoEntreFrames = 25;
	bool morto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 10;
	const int maxTempoPulando =3;
	int tempoPulando = 0;
	bool estaPulando = false;
	const int forcaPulo =40;
	const int aberturaMinima = 200;
	int score = 0;




	public MainPage()
	{
		InitializeComponent();
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
	}
	protected override void OnSizeAllocated(double width, double height)
	{
		base.OnSizeAllocated(width, height);
		larguraJanela = width;
		alturaJanela = height;
		if (height > 0);
	}
	void Inicializar()
	{
		passaro.TranslationY = 0;
		passaro.TranslationX = 0;
		score = 0;
		SoundHelper.Play("inicio.mp3");
		canodebaixo.TranslationY =-larguraJanela;
		canodecima.TranslationX =-larguraJanela;

		GerenciaCanos();
	}


	private async Task Desenhar()
	{
		while (!morto)
		{
			if (estaPulando)
				AplicarPulo();
			else 
				AplicarGravidade();

			GerenciaCanos();

			
			if (VerificaColisao())
			{
				morto = true;
				SoundHelper.Play("morte.mp3");
				labelLP.Text = "Você passou por " + score + "canos";
				frameGameOver.IsVisible = true;
				break;
			}
			await Task.Delay(tempoEntreFrames);
		}
	}
	void GerenciaCanos()
	{
		canodecima.TranslationX -= velocidade;
		canodebaixo.TranslationX -= velocidade;
		if (canodebaixo.TranslationX < -larguraJanela)
		{
			canodebaixo.TranslationX = 0;
			canodecima.TranslationX = 0;

			var alturaMax =-100;
			var alturaMin =-canodebaixo.HeightRequest;

			canodecima.TranslationY = Random.Shared.Next((int)alturaMin,(int)alturaMax);
			canodebaixo.TranslationY = canodecima.TranslationY + aberturaMinima + canodebaixo.HeightRequest;

			score++;
			labelLP.Text = "canos: "+score.ToString("D5");
			if (score % 4 == 0)
			    velocidade++;
		}
	}
	void AplicarGravidade()
	{
		passaro.TranslationY += gravidade;
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
	bool VerificaColisao()
	{
		{
			return VerificaColisaoTeto() ||
				VerificaColisaoChao() ||
				VerificaColisaoCanoCima()||
				VerificaColisaoCanoBaixo();	
		}
	}
	bool VerificaColisaoCano()
	{
		if (VerificaColisaoCanoBaixo()|| VerificaColisaoCanoCima())
			return true;
		else
			return false;
	}
	bool VerificaColisaoCanoCima()
	{
		var posHpassaro = (larguraJanela/2)-(passaro.WidthRequest/2);
		var posVpassaro = (alturaJanela/2)-(passaro.HeightRequest/2)+passaro.TranslationY;

		if (
			posHpassaro>= Math.Abs(canodecima.TranslationX)-canodecima.WidthRequest &&
		    posHpassaro<= Math.Abs(canodecima.TranslationX)+canodecima.WidthRequest &&
			posVpassaro<= canodecima.HeightRequest +canodecima.TranslationY
		)
				return true;
			else
				return false;
	}
	bool VerificaColisaoCanoBaixo()
	{
		var posHpassaro = (larguraJanela/2)-(passaro.WidthRequest/2);
		var posVpassaro = (alturaJanela/2)-(passaro.HeightRequest/2)+passaro.TranslationY;

		var yMaxCano = canodecima.HeightRequest + canodecima.TranslationY + aberturaMinima;

		if (
			posHpassaro>= Math.Abs(canodebaixo.TranslationX)-canodebaixo.WidthRequest &&
		    posHpassaro<= Math.Abs(canodebaixo.TranslationX)+canodebaixo.WidthRequest && 
			posVpassaro>=yMaxCano
			)
				return true;
			else
				return false;
	}
	bool VerificaColisaoChao()
	{
		var mixY = alturaJanela / 2; 
		if (passaro.TranslationY >=mixY)
	 		return true;
		else
			return false;
	}
	 private bool VerificaColisaoTeto()
	{
		var minY =-alturaJanela / 2;
		if (passaro.TranslationY <=-minY)

	 	    return true;
	    else
			return false;
	}

	private void OnGameOverClicked(object s, TappedEventArgs e)
	{
		frameGameOver.IsVisible = false;
		Inicializar();
		morto = false;
		Desenhar();
	}
	
	private void OnGridClickd(object s,TappedEventArgs e)
	{
		estaPulando = true;
	}

}