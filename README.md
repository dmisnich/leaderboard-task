# Unity Developer Test Task - Leaderboard Popup

The work was done in Unity 2021.3.27f1.

## How my solution works.

My solution is built on the `Zenject` framework, using `Addresables` and `async/await`.
I have a `LeaderboardModel` that loads data from a JSON file and defines several methods for managing this data.
There is also a `LeaderboardPopup` that displays our leaderboard and asynchronously loads avatars for each user.

## Changes

I made some changes to the `PopupManagerServiceService` file to make it extensible, so I created a child class called `PopupManagerServiceExtension` where I added my own logic without modifying the existing code. While there are many areas in the `PopupManager` that could be improved, I didn't address them in this prototype.