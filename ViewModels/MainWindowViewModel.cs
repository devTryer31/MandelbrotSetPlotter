using MandelbrotSetPlotter.Base.ViewModels;
using MandelbrotSetPlotter.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotSetPlotter.ViewModels {
	sealed class MainWindowViewModel : ViewModel {

		private const uint PlotHeight = 800, PlotWidth = 800;

		private const double R = 6;

		private double[,] _PlotData = new double[PlotWidth, PlotHeight];

		public double[,] PlotData {
			get {
				OnPropertyChanged(nameof(PlotData));
				return _PlotData;
			}
			set => Set(ref _PlotData, value);
		}

		private PlotModel _Model = new PlotModel { Title = "Mandelbrot Set"};

		public PlotModel Model {
			get => _Model;
			set => Set(ref _Model, value);
		}

		public MainWindowViewModel() {

			Model.Axes.Add(new LinearColorAxis {
				Palette = OxyPalettes.Rainbow(100),
				IsZoomEnabled = false
			});

			for (int x = 0; x < PlotWidth; ++x) {
				for (int y = 0; y < PlotHeight; ++y) {
					//Normalization
					double a = (x - PlotWidth / 2d) / (PlotWidth / 5);		//real
					double b = (y - PlotHeight / 2d) / (PlotHeight / 5);	//imag parts 
					ComplexNumber c = new ComplexNumber(a, b),
						z = new ComplexNumber(0, 0); //Start point
					int n = 0;
					while (++n < Math.Min(PlotHeight, PlotWidth)) {
						z = z.Square() + c;
						if (!(z.Abs() < R)) //|Zn| < R
							break;
					}

					//It point out
					if (n < 100)
						PlotData[x, y] = z.Abs();
					else
						PlotData[x, y] = z.Abs() * ((x * a) / (b * y))/100;
				}
			}


			var heatMapSeries = new HeatMapSeries {
				X0 = 0,
				X1 = PlotWidth - 1,
				Y0 = 0,
				Y1 = PlotHeight - 1,
				Interpolate = true,
				RenderMethod = HeatMapRenderMethod.Bitmap,
				Data = PlotData
			};
			Model.Series.Add(heatMapSeries);
		}
	}
}
