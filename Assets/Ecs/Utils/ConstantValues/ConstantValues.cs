namespace Ecs.Utils.ConstantValues
{
    public static class ConstantValues
    {
        public const float BLOCKS_WIDTH = 1f;
        
        // коэффициент, нужный для формулы, которая высчитывает offset между пайвотами фигуры и линией, в которую упрется эта фигура
        public const float COEFFICIENT_BETWEEN_PIVOTS = 2.5f;
        
        // оффсеты, нужные для формулы, которая высчитывает смещение фигуры между пайвотом префаба и геометрическим центром фигуры, используя данные матрицы фигуры
        // если, в будущем, понадобится менять размеры блока в префабе, то эти данные нужно вынести в SO или в сервис
        public const float FIGURE_OFFSET_COEFFICIENT = 1.5f;
        public const float FIGURE_OFFSET_Z = -0.5f;
    }
}