#r @"src\packages\FAKE.3.17.7\tools\FakeLib.dll"

open Fake

Target "Clean" (fun _ ->
    CleanDir @"src\bin"
)

Target "Build" (fun _ ->
    !! @"src\**\*.fsproj"
    |> MSBuild @"src\bin" "Build" []
    |> Log "Build log:"
)

Target "Test" (fun _ ->
    !! @"src\bin\*.dll"
    |> NUnit (fun p -> { p with DisableShadowCopy = true })
)

"Clean" ==> "Build" ==> "Test"

RunTargetOrDefault "Test"
