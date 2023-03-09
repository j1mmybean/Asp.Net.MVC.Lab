using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.NewFolder
{
	public class CLottoGen
	{
		public List<int> GetLottoNumbers(int count, int max)
		{
			Random random = new Random(Guid.NewGuid().GetHashCode());
			List<int> numbers = new List<int>();

			//判斷取得之number是否重複
			for (int i = 0; i < count; i++)
			{
				int number;
				do
				{
					number = random.Next(max);
				} while (numbers.Contains(number));
				numbers.Add(number);
			}
			//numbersTemp = numbersTemp.OrderBy(n => n).ToList();
			numbers.Sort();
			return numbers;
		}
	}
}
