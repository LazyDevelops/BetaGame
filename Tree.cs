using System;

namespace Tree {
    class Tree<T> {
        public T? Data;
        public Tree<T>? Parents;
        public Tree<T>? LeftChild;
        public Tree<T>? RightSibling;

        public Tree() { }

        public Tree(T data) {
            LeftChild = null;
            RightSibling = null;
            Data = data;
        }

        public void AppendChildNode(Tree<T> child) {
            if (LeftChild == null) {
                LeftChild = child;
            } else {
                Tree<T> temp = LeftChild;

                while (temp.RightSibling != null) {
                    temp = temp.RightSibling;
                }

                temp.RightSibling = child;
            }
            
            child.Parents = this;
        }

        public List<Tree<T>> GetChildren()
        {
            List<Tree<T>> children = new List<Tree<T>>();
            Tree<T>? child = LeftChild;

            while (child != null)
            {
                children.Add(child);
                child = child.RightSibling;
            }

            return children;
        }

        public void RemoveChildNode(Tree<T> nodeToRemove)
        {
            if (nodeToRemove == null)
            {
                return; // 삭제할 노드가 null인 경우 종료
            }

            if (LeftChild == nodeToRemove)
            {
                // 삭제할 노드가 첫 번째 자식인 경우
                LeftChild = nodeToRemove.RightSibling;
            }
            else
            {
                // 삭제할 노드가 첫 번째 자식이 아닌 경우
                Tree<T>? currentChild = LeftChild;
                while (currentChild != null)
                {
                    if (currentChild.RightSibling == nodeToRemove)
                    {
                        // 삭제할 노드를 찾은 경우
                        currentChild.RightSibling = nodeToRemove.RightSibling;
                        return;
                    }
                    currentChild = currentChild.RightSibling;
                }
            }
        }

        public void PrintTree(int depth) {
            for (int i = 0; i < depth; i++) {
                Console.Write(" ");
            }

            Console.WriteLine(Data);

            if (LeftChild != null) {
                LeftChild.PrintTree(depth + 1);
            }

            if (RightSibling != null) {
                RightSibling.PrintTree(depth);
            }
        }
    }
}