# Court Hearing Schedule

This asp.net webapp displays a court hearing schedule and then assigns automated hearing numbers based on predefined rules. Each department has its own hearing numbers, Numbers are assigned sequential withing a time slot ordered by hearing type then by case number. Once a hearing number is assigned it cannot be changed and new hearings are assigned the next sequential number in its time slot.

## Set-up

Navigate to the CourtHearingSchedule directory and run the restore command to set-up the project

```bash
dotnet restore
```

## Usage

Navigate to the CourtHearingSchedule directory

```bash
Usage: dotnet run
```

Navigate to `http://127.0.0.1:5000` to see the app in action!

## Screenshots

![Court Hearing Schedule before day start](https://i.imgur.com/8zNo84r.png)

![Court Hearing Schedule after day start](https://i.imgur.com/G8WXgl3.png)

![Court Hearing Schedule after day start with new hearing](https://i.imgur.com/LK4FtXr.png)

## Comments

You can reset the day to remove hearing numbers to start over. A sample sqlite database will be initialized automatically. To reset the database, simply delete and it will re-initialize to the sample data.

If you wanted to add another valid time slot, all that would be required would be to add the time to the list in Models/Hearing.cs.
