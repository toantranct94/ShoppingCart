find . -type d -name TestResults -exec rm -rf {} \;
dotnet test --collect:"XPlat Code Coverage"
folder=$(find . -type d -name TestResults -exec ls -1 {} \; | sort -n | tail -1)
reportgenerator -reports:"ShoppingCart.Tests/TestResults/$folder"/coverage.cobertura.xml -targetdir:"coveragereport" -reporttypes:Html

html_file_path="coveragereport/index.html"
if [ -f "$html_file_path" ]; then
    open "$html_file_path"  # This is for Linux systems
else
    echo "HTML file not found at: $html_file_path"
fi