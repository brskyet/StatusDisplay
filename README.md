# StatusDisplay
StatusDisplay is a cross-platform project for displaying information from various services like Trello, Yandex.Weather, etc.
# Overview
## Information displaying
The main window looks like this:
<p align="center">
 <img width="800" height="450" align="center" src="https://funkyimg.com/i/35ipc.png">
</p>
Almost all of the elements are interractive. Time gets from current OS. Weather forecast takes from [Yandex.Weather](https://yandex.ru/pogoda/) API. You can push on this element and see extended information:
<p align="center">
 <img width="800" height="450" align="center" src="https://funkyimg.com/i/35ipi.png">
</p>
You are able to set time on timer, which is below weather forcast. Also you can scroll list of tasks in right upper corner and mark some of them as complete. This list takes from [Trello](https://trello.com/) API. Below todo list there is latest news from various resources. If you touch this element you can see extended list of news:
<p align="center">
 <img width="800" height="450" align="center" src="https://funkyimg.com/i/35ipj.png">
</p>
And last element is «Word of the day» from [Wordnik](https://www.wordnik.com/word-of-the-day) API. Extended widget looks like this:
<p align="center">
 <img width="800" height="450" align="center" src="https://funkyimg.com/i/35ipk.png">
</p>
## Technology stack
* [ASP.NET Core](https://github.com/dotnet/aspnetcore)
* [AvaloniaUI](https://github.com/AvaloniaUI)