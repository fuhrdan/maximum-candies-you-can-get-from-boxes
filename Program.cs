//*****************************************************************************
//** 1298. Maximum Candies You Can Get from Boxes                   leetcode **
//*****************************************************************************

int maxCandies(int* status, int statusSize, int* candies, int candiesSize, int** keys, int keysSize, int* keysColSize, int** containedBoxes, int containedBoxesSize, int* containedBoxesColSize, int* initialBoxes, int initialBoxesSize) {
    int retval = 0;
    int queue[50000], front = 0, back = 0;
    int hasBox[50000] = {0};
    int hasKey[50000] = {0};
    int visited[50000] = {0}; // whether we have opened and processed the box

    for (int i = 0; i < initialBoxesSize; i++)
    {
        int box = initialBoxes[i];
        hasBox[box] = 1;
        queue[back++] = box;
    }

    while (front < back)
    {
        int progress = 0;
        int newQueue[50000], newFront = 0, newBack = 0;

        while (front < back)
        {
            int box = queue[front++];

            if (!status[box] && !hasKey[box])
            {
                newQueue[newBack++] = box;
                continue;
            }

            if (visited[box])
            {
                continue;
            }

            visited[box] = 1;
            retval += candies[box];
            progress = 1;

            for (int i = 0; i < keysColSize[box]; i++)
            {
                int key = keys[box][i];
                hasKey[key] = 1;
                if (hasBox[key] && !visited[key])
                {
                    newQueue[newBack++] = key;
                }
            }

            for (int i = 0; i < containedBoxesColSize[box]; i++)
            {
                int newBox = containedBoxes[box][i];
                hasBox[newBox] = 1;
                newQueue[newBack++] = newBox;
            }
        }

        front = 0;
        back = 0;
        for (int i = 0; i < newBack; i++)
        {
            queue[back++] = newQueue[i];
        }

        if (!progress)
        {
            break;
        }
    }

    return retval;
}