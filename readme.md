
```markdown
# High-Performance JSON Analyzer

```
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
```

A powerful and efficient web-based tool built with ASP.NET Core for analyzing massive JSON files. This application is designed to process files hundreds of megabytes in size without overwhelming server memory, by leveraging a streaming-based architecture.

It provides a user-friendly interface to count occurrences of specific properties and values, validate raw JSON text, and visualize data structures in an interactive tree view.

```

<p align="center">
  <img src="https://github.com/mauandrade99/JsonAnalyzer/blob/main/image.png?raw=true" width="750">
</p>

```


## ✨ Key Features

-   **Handles Massive Files:** Processes large JSON files (tested up to 250MB+) with minimal and constant memory usage thanks to a streaming API.
-   **Flexible Property Analysis:**
    -   Count occurrences of any property (key) throughout a file.
    -   Filter counts by a specific value.
    -   **Multi-value Search:** Search for multiple values at once using a `|` separator (e.g., `active|paused`).
    -   Correctly handles `null` and empty string values.
-   **Live JSON Validator:** A dedicated panel to paste raw JSON text, validate its syntax, and get precise error feedback (including line and position).
-   **Interactive Tree Viewer:** Visualize the structure of smaller JSON files or validated text in a collapsible, interactive tree view powered by `vanilla-jsoneditor`.
-   **Modern User Experience:** The interface uses AJAX for all operations, meaning no page reloads. The selected file remains in place for multiple analyses.
-   **Production-Ready:** Configured to run correctly behind an IIS server with a custom `PathBase`, with increased request size limits.
-   **Internationalized UI:** The entire user interface is in English.

## 🚀 Tech Stack

-   **Backend:** C# & ASP.NET Core Razor Pages (.NET 6+)
-   **JSON Processing:** `Newtonsoft.Json` (Json.NET) for robust, high-performance streaming.
-   **Frontend:** HTML5, CSS3, Bootstrap 5, JavaScript (ES Modules, Fetch API).
-   **JSON Tree View:** `vanilla-jsoneditor`
-   **Web Server:** Kestrel (configured for IIS hosting).

## ⚙️ Getting Started

Follow these instructions to get the project running on your local machine.

### Prerequisites

-   [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or newer.
-   [Node.js and npm](https://nodejs.org/) (used to manage frontend dependencies).

### Installation & Execution

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/mauandrade99/JsonAnalyzer.git
    cd JsonAnalyzer
    ```

2.  **Install frontend dependencies:**
    This command reads the `package.json` file and downloads the `vanilla-jsoneditor` library into the `node_modules` folder.
    ```bash
    npm install
    ```
    *(The required JS/CSS files from `node_modules` are already copied into the `wwwroot/lib` folder in this repository, but running `npm install` is good practice.)*

3.  **Restore .NET dependencies:**
    This downloads `Newtonsoft.Json` and other required .NET packages.
    ```bash
    dotnet restore
    ```

4.  **Run the application:**
    ```bash
    dotnet run
    ```

5.  **Open in your browser:**
    The console will display the URLs the application is listening on. Open one of them, typically `https://localhost:7164`.

## 📄 How It Works

This application was engineered to solve the primary challenge of large file processing: memory consumption.

-   **Increased Request Limits:** The `Program.cs` (for Kestrel) and `web.config` (for IIS) files are configured to allow large file uploads (up to 500 MB).
-   **Streaming API:** The core of the backend uses `Newtonsoft.Json.JsonTextReader`. Instead of loading the entire file into memory (`string` or `JObject`), it reads the file token-by-token from a `Stream`, ensuring memory usage remains low and stable regardless of file size.
-   **AJAX & Partial Views:** The frontend communicates with the server via the JavaScript Fetch API. The server processes requests and returns small HTML snippets (Partial Views) containing only the results, which are then injected into the page. This prevents full-page reloads and provides a smooth user experience.
-   **Robust Path Resolution:** The application uses the `@Url.Content()` Razor helper to correctly resolve paths to static assets (like the JSON editor script), ensuring it works flawlessly both in local development and when deployed under a sub-path in IIS.

## 📜 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
```