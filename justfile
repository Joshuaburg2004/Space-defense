message := "Committing current files"
run:
    dotnet run --project SpaceDefence
git-routine:
    git add . && git commit -m "{{message}}" && git push
