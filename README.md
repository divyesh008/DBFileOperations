<!--- This will not show because it's COMMENT
- https://stackoverflow.com/questions/47344571/how-to-draw-checkbox-or-tick-mark-in-github-markdown-table
- https://stackoverflow.com/questions/4823468/comments-in-markdown#:~:text=Using%20%23%20(and%20not,no%20impact%20on%20the%20result.
- https://stackoverflow.com/questions/11509830/how-to-add-color-to-githubs-readme-md-file
- https://docs.github.com/en/github/writing-on-github/basic-writing-and-formatting-syntax
- https://guides.github.com/features/mastering-markdown/
- https://docs.github.com/en/github/writing-on-github/autolinked-references-and-urls
--->

# DBFileOperations
- Download document 
- Save it to Device's special folder 
- Preview any type of document (i.e. Image, PDF, Excel, Word) without 3rd party library like **Syncfusion's PdfViewerControl**

### Platform Support
- [x] iOS
- [x] Android 
  
### Document Types
Support | Name
:---:| ---
✅| Image 
✅| PDF
✅| Excel, Word 
✅| RTF, PlainText

### iOS Setup 
> **Note:** I have not implemented Document picker code in this project. But it's really important, so keep below mentioned things in mind:

#### For picking documents we have to use
1. UIDocumentPickerViewController in Xamarin.iOS (Native code)
2. Xamarin.Plugin.FilePicker in Xamarin.Forms

But in both case before the Document Picker can be used you have to **enable iCloud support both in your application and via Apple i.e you have to add iCould Container in you App Id.**

> **The following steps walkthrough the process of provisioning for iCloud.**

- Create an iCloud Container.
- Create an App ID that contains the iCloud App Service.
- Create a Provisioning profile that includes this App ID.


#### Now let's dive into Setup required for Document Previwer in iOS:

:heavy_check_mark:  `Add this permissions into your Info.plist file:` 
```js
<key>ITSAppUsesNonExemptEncryption</key>
<false/>
<key>LSSupportsOpeningDocumentsInPlace</key>
<true/>
<key>UIFileSharingEnabled</key>
<true/> 
```

:heavy_check_mark:  `Make this changes in Entitlements.plist file:`
```js
<dict>
    <key>com.apple.developer.icloud-services</key>
    <array>
        <string>CloudDocuments</string>
    </array>
    <key>com.apple.developer.icloud-container-identifiers</key>
    <array>
        <string>iCloud.com.yourcompany.yourapp</string>
    </array>
    <key>com.apple.developer.ubiquity-container-identifiers</key>
    <array/>
</dict>
```
> With this you also have to enable this option in Entitlements.plist file: 

1. Enable iCould
  - Key-value storage 
  - iCouldDocuments 
  - CloudKit
  
2. Enable Keychain


### Android Setup 

> **Permissions:**

Permission Name | Required
:--- | :---:
ReadExternalStorage|    ✅  
WriteExternalStorage|   ✅     
Internet|               ✅
 

```c#
readonly string[] Permissions =
{
    Android.Manifest.Permission.ReadExternalStorage,
    Android.Manifest.Permission.WriteExternalStorage,
    Android.Manifest.Permission.Internet,
};
```

```diff
@@ For apps that target Android 5.1(API level 22) or lower, there is nothing more that needs to be done. @@
@@ Apps that will run on Android 6.0(API 23 level 23) or higher should ask Run time permission checks. @@
- Handles this Exception: Xamarin: Android: 
- System.UnauthorizedAccessException: Access to the path is denied
```
  
```c#
if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
{
    if (!(CheckPermissionGranted(Manifest.Permission.ReadExternalStorage) && !CheckPermissionGranted(Manifest.Permission.WriteExternalStorage)))
    {
       ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 0);
    }
}
```

## Bonus Idea: If you don't want to perform "Missing Compliance" process manually everytime when you add build to Testflight add this permission in Info.plist file (https://stackoverflow.com/questions/35841117/missing-compliance-in-status-when-i-add-built-for-internal-testing-in-test-fligh)

```js
<key>ITSAppUsesNonExemptEncryption</key>
<false/>  
```

## Further reading for iOS
- https://docs.microsoft.com/en-us/xamarin/ios/deploy-test/provisioning/capabilities/icloud-capabilities
- http://babbacom.com/?p=318
- https://docs.microsoft.com/en-us/xamarin/ios/platform/document-picker
- https://www.c-sharpcorner.com/article/how-to-pick-a-document-in-xamarin-ios/

## Further reading for Xamarin.Forms
- https://www.c-sharpcorner.com/article/how-to-create-file-picker-in-xamarin-forms/
- https://github.com/jfversluis/FilePicker-Plugin-for-Xamarin-and-Windows
- https://techsolutions2017.blogspot.com/2017/02/filepicker-in-xamarin-forms.html
