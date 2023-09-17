// Необходимо преобразовать собранное на семинаре дерево поиска в полноценное левостороннее
// красно-чёрное дерево. Реализовать метод добавления новых элементов с балансировкой.

namespace lesson4
{
    class Tree
    {
        private Node root;

        // Класс для узла дерева
        private class Node
        {
            public int value;
            public Node left;
            public Node right;
            public bool isRed; // Добавляем поле для цвета узла

            public Node(int value)
            {
                this.value = value;
                this.isRed = true; // Новые узлы всегда красные
            }
        }

        public bool Contain(int value)
        {
            return Contain(value, root);
        }

        private bool Contain(int value, Node node)
        {
            if (node == null)
                return false;

            if (node.value == value)
                return true;

            if (value < node.value)
                return Contain(value, node.left);

            return Contain(value, node.right);
        }

        public void Insert(int value)
        {
            root = Insert(root, value);
            root.isRed = false; // Корень всегда черный
        }

        private Node Insert(Node node, int value)
        {
            if (node == null)
                return new Node(value);

            if (value < node.value)
                node.left = Insert(node.left, value);
            else if (value > node.value)
                node.right = Insert(node.right, value);

            // Балансировка после вставки
            if (IsRed(node.right) && !IsRed(node.left)) // Проверка: правый дочерний красный, левый черный
                node = RotateLeft(node); // Малый правый поворот

            if (IsRed(node.left) && IsRed(node.left.left)) // Проверка: левый дочерний и его левый дочерний красные
                node = RotateRight(node); // Малый левый поворот

            if (IsRed(node.left) && IsRed(node.right)) // Проверка: оба дочерних красные
                FlipColors(node); // Смена цвета

            return node;
        }

        private Node RotateLeft(Node node)
        {
            Node newRoot = node.right;
            node.right = newRoot.left;
            newRoot.left = node;
            newRoot.isRed = node.isRed;
            node.isRed = true;
            return newRoot;
        }

        private Node RotateRight(Node node)
        {
            Node newRoot = node.left;
            node.left = newRoot.right;
            newRoot.right = node;
            newRoot.isRed = node.isRed;
            node.isRed = true;
            return newRoot;
        }

        private void FlipColors(Node node)
        {
            node.isRed = true;
            node.left.isRed = false;
            node.right.isRed = false;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
                return false;
            return node.isRed;
        }
    }
}