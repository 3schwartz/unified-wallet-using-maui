# Interops

## iOS

Download `ibmobile_wallet_ios.a` and `ibmobile_wallet_ios_simulator.a` from https://s3.eu-west-1.amazonaws.com/static-libraries.concordium.com/iOS/libmobile_wallet_0.24.0-0.xcframework.zip and store at `Platforms/iOS/lib/*`.

From here: https://github.com/Concordium/concordium-wallet-crypto-swift/blob/main/Package.swift

### Tips and tricks
- https://github.com/dotnet/maui/issues/12675
- https://www.mono-project.com/docs/advanced/pinvoke/

## Android

Follow build instruction https://github.com/Concordium/concordium-base/blob/e7ccbd2f1cdcce85f911eb184d4d2fe03388fc68/mobile_wallet/README.md to generated `*.so`-files used by Android. Important script is [`build-android.sh`](https://github.com/Concordium/concordium-base/blob/e7ccbd2f1cdcce85f911eb184d4d2fe03388fc68/mobile_wallet/android/build-android.sh).

### Tips and tricks
- https://learn.microsoft.com/en-us/xamarin/android/platform/native-libraries
- https://stackoverflow.com/questions/73500511/how-to-include-existing-c-libraries-per-platform-in-maui-project
