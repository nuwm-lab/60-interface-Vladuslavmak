using System;
using System.Globalization;

namespace EllipseTask
{
    // 🔹 Інтерфейс для геометричних об’єктів
    interface IShape
    {
        bool IsPointOnCurve(double x, double y);
        void DisplayInfo();
    }

    // 🔹 Абстрактний клас: крива другого порядку
    abstract class QuadraticCurve : IShape
    {
        protected const double EPS = 1e-6;

        private double _a11, _a12, _a22, _b1, _b2, _c;

        // 🔸 Властивості
        public double A11 => _a11;
        public double A12 => _a12;
        public double A22 => _a22;
        public double B1 => _b1;
        public double B2 => _b2;
        public double C => _c;

        // 🔸 Конструктор
        public QuadraticCurve(double a11, double a12, double a22, double b1, double b2, double c)
        {
            _a11 = a11;
            _a12 = a12;
            _a22 = a22;
            _b1 = b1;
            _b2 = b2;
            _c = c;
            Console.WriteLine("Створено об’єкт класу QuadraticCurve");
        }

        // 🔸 Деструктор
        ~QuadraticCurve()
        {
            Console.WriteLine("Звільнено об’єкт QuadraticCurve");
        }

        // 🔸 Абстрактні методи (обов’язкові для реалізації)
        public abstract bool IsPointOnCurve(double x, double y);

        public abstract void DisplayInfo();

        // 🔸 Загальний метод
        public virtual void DisplayCoefficients()
        {
            Console.WriteLine($"Коефіцієнти: a11={_a11}, a12={_a12}, a22={_a22}, b1={_b1}, b2={_b2}, c={_c}");
        }
    }

    // 🔹 Клас еліпса
    class Ellipse : QuadraticCurve
    {
        private double _a, _b;

        public double A => _a;
        public double B => _b;

        // Конструктор
        public Ellipse(double a, double b)
            : base(1 / (a * a), 0, 1 / (b * b), 0, 0, -1)
        {
            _a = a;
            _b = b;
            Console.WriteLine("Створено еліпс");
        }

        // Деструктор
        ~Ellipse()
        {
            Console.WriteLine("Знищено еліпс");
        }

        public override bool IsPointOnCurve(double x, double y)
        {
            double value = (x * x) / (_a * _a) + (y * y) / (_b * _b);
            return Math.Abs(value - 1) < EPS;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"\nЕліпс: (x² / {_a * _a}) + (y² / {_b * _b}) = 1");
            base.DisplayCoefficients();
        }
    }

    // 🔹 Клас гіперболи
    class Hyperbola : QuadraticCurve
    {
        private double _a, _b;

        public double A => _a;
        public double B => _b;

        // Конструктор
        public Hyperbola(double a, double b)
            : base(1 / (a * a), 0, -1 / (b * b), 0, 0, -1)
        {
            _a = a;
            _b = b;
            Console.WriteLine("Створено гіперболу");
        }

        // Деструктор
        ~Hyperbola()
        {
            Console.WriteLine("Знищено гіперболу");
        }

        public override bool IsPointOnCurve(double x, double y)
        {
            double value = (x * x) / (_a * _a) - (y * y) / (_b * _b);
            return Math.Abs(value - 1) < EPS;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"\nГіпербола: (x² / {_a * _a}) - (y² / {_b * _b}) = 1");
            base.DisplayCoefficients();
        }
    }

    // 🔹 Основна програма
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== ПРОГРАМА: Абстрактні класи та інтерфейси ===");

            try
            {
                double a = ReadPositiveDouble("Введіть a (піввісь по осі X): ");
                double b = ReadPositiveDouble("Введіть b (піввісь по осі Y): ");

                // Використання інтерфейсу
                IShape[] shapes =
                {
                    new Ellipse(a, b),
                    new Hyperbola(a, b)
                };

                Console.WriteLine("\nВведіть координати точки:");
                double x = ReadDouble("x = ");
                double y = ReadDouble("y = ");

                foreach (var shape in shapes)
                {
                    shape.DisplayInfo();
                    Console.WriteLine(shape.IsPointOnCurve(x, y)
                        ? $"Точка ({x}, {y}) належить кривій."
                        : $"Точка ({x}, {y}) не належить кривій.");
                    Console.WriteLine(new string('-', 60));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Помилка: {ex.Message}");
            }

            Console.WriteLine("\n=== Кінець роботи програми ===");
        }

        // 🔸 Валідація: позитивне число
        static double ReadPositiveDouble(string message)
        {
            double value;
            do
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out value) && value > 0)
                    return value;
                Console.WriteLine("Помилка: потрібно ввести додатне число!");
            } while (true);
        }

        // 🔸 Валідація: будь-яке число
        static double ReadDouble(string message)
        {
            double value;
            do
            {
                Console.Write(message);
                if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out value))
                    return value;
                Console.WriteLine("Помилка: введіть коректне число!");
            } while (true);
        }
    }
}
