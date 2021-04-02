# <b>Integrated Developmet Environments</b>

## <b>Task 3-4</b>

Following documentation will decribe all releases and recieved results.

## <b>Release v0.1</b>

Implemented fuctionality:

* Create Student from console.
* Display Students as table showing final pints unsing homework average or median.
* Add randomly generated Student to the list.

## <b>Release v0.2</b>

Implemented fuctionality:

* Read list of Students from `.csv` file.
* Display Students as a table sorted by Surname.

## <b>Release v0.3</b>

Implemented fuctionality:

* Exception handling.

## <b>Release v0.4</b>

Implemented fuctionality:

* Generation of 10_000, 100_000, 1_000_000 and 10_000_000 random Students and splitting them into 2 `.csv` files depending on Final Result.

<b>Performance testing result:</b>

![plot](Assets/test_generation.PNG?raw=true 'Performance testing results')


## <b>Release v0.5</b>

Using the same dataset Student sorting was performed using `List<>`, `LinkedList<>` and `Queue<>`, after sorted into 2 groups students were saved into `.csv` files depending on their Final Result.

<b>Performance testing result:</b>

![plot](Assets/sorting_and_saving_to_files.PNG?raw=true 'Performance testing results')

### <b>Analysis:</b>

* `List<>` performed best operating small amounts of data
* `LinkedList<>` performed worst operating large amount of data
* `Queue<>` performed best operating large amount of data

## <b>Release v1.0</b>

Using the same dataset Student sorting was performed using `List<>`, `LinkedList<>` and `Queue<>`. Students then were sorted into 2 groups depending on their Final Result.

Sorting was performed using 2 strategies:

* <b>Strategy 1.</b> Copy failed and passed students into two new containers of the same type as starting student collection.

* <b>Strategy 1.</b> Failed student has to be added inot new failed students collection while being removed from starting student collection, this way after analyzing all the students only passed students will remain in the starting collection.

While strategy 1 is focused on speed, strategy 2 is more memory efficient.

<b>Performance testing result:</b>

![plot](Assets/sorting_diff_strategies.PNG?raw=true 'Performance testing results')

### <b>Analysis:</b>

* Strategy 1 proved to be much faster performing from 1_000 to 1_000_000 times better than strategy 2.
* Unexpected result is that strategy 1 proved to work faster with larger amount of data. E.g. sorting 10_000 students using `List<>` took `42,9 µs`, using the same collection to sort 10_000_000 students took only `1,3 µs`.
