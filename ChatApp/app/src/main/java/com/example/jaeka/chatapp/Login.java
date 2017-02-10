package com.example.jaeka.chatapp;


import android.app.ProgressDialog;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.io.OutputStreamWriter;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;

public class Login extends AppCompatActivity {
    private EditText etUsername;
    private EditText etPassword;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        etUsername = (EditText) findViewById(R.id.Username);
        etPassword = (EditText) findViewById(R.id.Password);
    }

    public void checkLogin(View view){
        final String Username = etUsername.getText().toString();
        final String Password = etPassword.getText().toString();

        new AsyncLogin().execute(Username,Password);
    }

    private class AsyncLogin extends AsyncTask<String,String,String>{
        ProgressDialog pdLoading = new ProgressDialog(Login.this);
        HttpURLConnection conn;
        URL url = null;

        @Override
        protected void onPreExecute(){
            super.onPreExecute();

            pdLoading.setMessage("\tLoading...");
            pdLoading.setCancelable(false);
            pdLoading.show();
        }
        @Override
        protected String doInBackground(String... params){
            String whatev = null;
            try{
                url = new URL("http://chatton.azurewebsites.net/api/user/Androidlogin/");
            }catch (MalformedURLException e){
                e.printStackTrace();
                return "exception";
            }
            try{
                conn = (HttpURLConnection) url.openConnection();
                conn.setRequestMethod("POST");
                conn.setRequestProperty("Content-Type","application/json");

                conn.setDoInput(true);
                conn.setDoOutput(true);

                String Username = params[0];
                String Password = params[1];
                String query = "{'Username':'"+ Username +"','Password':'"+ Password+"' }";
                OutputStream os = conn.getOutputStream();
                BufferedWriter writer = new BufferedWriter(new OutputStreamWriter(os, "UTF-8"));
                writer.write(query);
                writer.flush();
                writer.close();
                os.close();
                conn.connect();
            }catch (IOException e1){
                e1.printStackTrace();
                return "exception";
            }

            try{
                int response_code = conn.getResponseCode();

                if(response_code == HttpURLConnection.HTTP_OK){
                    InputStream input = conn.getInputStream();
                    BufferedReader reader = new BufferedReader(new InputStreamReader(input));
                    StringBuilder result = new StringBuilder();
                    String line;

                    while((line = reader.readLine()) != null){
                        result.append(line);
                    }
                    whatev = result.toString();
                    //return (result.toString());
                }else {
                    whatev = "unsuccessful";
                    //return ("unsuccessful");
                }
                conn.disconnect();
            }catch (IOException e){
                e.printStackTrace();
                return "exception";
            }

            return whatev;
        }

        @Override
        protected void onPostExecute(String result){
            pdLoading.dismiss();
            if (result != null){
                Intent intent = new Intent(Login.this, Chat.class);
                startActivity(intent);
                Login.this.finish();
            }else {
                Toast.makeText(Login.this, "Invalid username or password", Toast.LENGTH_LONG);
            }
        }
    }
}